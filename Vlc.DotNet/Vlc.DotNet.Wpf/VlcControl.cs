using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Wpf
{
    /// <summary>
    /// VlcControl class
    /// </summary>
    public sealed partial class VlcControl : FrameworkElement
    {
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.LockCallbackDelegate myVideoLockCallback;
        private GCHandle myVideoLockCallbackHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.UnlockCallbackDelegate myVideoUnlockCallback;
        private GCHandle myVideoUnlockCallbackHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.DisplayCallbackDelegate myVideoDisplayCallback;
        private GCHandle myVideoDisplayCallbackHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.FormatCallbackDelegate myVideoSetFormat;
        private GCHandle myVideoSetFormatHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.CleanupCallbackDelegate myVideoCleanup;
        private GCHandle myVideoCleanupHandle;

        private VlcControlWpfRendererContext vlcControlWpfRendererContext;

        /// <summary>
        /// Identifies the Vlc.DotNet.Wpf.VideoSource dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoSourceProperty = DependencyProperty.Register("VideoSource", typeof(ImageSource), typeof(VlcControl));

        /// <summary>
        /// Gets or sets the System.Windows.Media.ImageSource for the video. This is a dependency property.
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public ImageSource VideoSource
        {
            get { return (ImageSource)GetValue(VideoSourceProperty); }
            set { SetValue(VideoSourceProperty, value); }
        }

        /// <summary>
        /// Identifies the Vlc.DotNet.Wpf.VideoBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoBrushProperty = DependencyProperty.Register("VideoBrush", typeof(ImageBrush), typeof(VlcControl));

        /// <summary>
        /// Gets or sets the System.Windows.Media.ImageBrush for the video. This is a dependency property.
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public ImageBrush VideoBrush
        {
            get { return (ImageBrush)GetValue(VideoBrushProperty); }
            set { SetValue(VideoBrushProperty, value); }
        }

        /// <summary>
        /// Constructor of VlcControl
        /// </summary>
        public VlcControl()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            VideoBrush = new ImageBrush();

            if (!VlcContext.IsInitialized)
                VlcContext.Initialize();

            VlcContext.HandleManager.MediaPlayerHandles[this] =
                VlcContext.InteropManager.MediaPlayerInterops.NewInstance.Invoke(
                    VlcContext.HandleManager.LibVlcHandle);
            AudioProperties = new VlcAudioProperties(this);
            VideoProperties = new VlcVideoProperties(this);
            LogProperties = new VlcLogProperties();
            InitEvents();

            myVideoLockCallback = LockCallback;
            myVideoLockCallbackHandle = GCHandle.Alloc(myVideoLockCallback);
            myVideoUnlockCallback = UnlockCallback;
            myVideoUnlockCallbackHandle = GCHandle.Alloc(myVideoUnlockCallback);
            myVideoDisplayCallback = DisplayCallback;
            myVideoDisplayCallbackHandle = GCHandle.Alloc(myVideoDisplayCallback);

            myVideoSetFormat = VideoSetFormat;
            myVideoSetFormatHandle = GCHandle.Alloc(myVideoSetFormat);
            myVideoCleanup = VideoCleanup;
            myVideoCleanupHandle = GCHandle.Alloc(myVideoCleanup);

            VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetFormatCallbacks.Invoke(
                VlcContext.HandleManager.MediaPlayerHandles[this], myVideoSetFormat, myVideoCleanup);
            VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetCallbacks.Invoke(
                VlcContext.HandleManager.MediaPlayerHandles[this], myVideoLockCallback, myVideoUnlockCallback, myVideoDisplayCallback, IntPtr.Zero);
        }

        #region Vlc Display

        private void LockCallback(IntPtr opaque, ref IntPtr plane)
        {
            var context = (VlcControlWpfRendererContext)Marshal.PtrToStructure(opaque, typeof(VlcControlWpfRendererContext));
            plane = context.Data;
        }
        private void UnlockCallback(IntPtr opaque, IntPtr picture, ref IntPtr plane)
        {

        }
        private void DisplayCallback(IntPtr opaque, IntPtr picture)
        {
            var wpfVlcControlRendererContext = (VlcControlWpfRendererContext)Marshal.PtrToStructure(opaque, typeof(VlcControlWpfRendererContext));
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action<VlcControlWpfRendererContext>(RenderImage), wpfVlcControlRendererContext);
        }
        private uint VideoSetFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines)
        {
            vlcControlWpfRendererContext = new VlcControlWpfRendererContext(width, height, PixelFormats.Bgr32);

            chroma = BitConverter.ToUInt32(new[] { (byte)'R', (byte)'V', (byte)'3', (byte)'2' }, 0);
            width = (uint)vlcControlWpfRendererContext.Width;
            height = (uint)vlcControlWpfRendererContext.Height;
            pitches = (uint)vlcControlWpfRendererContext.Stride;
            lines = (uint)vlcControlWpfRendererContext.Height;
            opaque = GCHandle.Alloc(vlcControlWpfRendererContext, GCHandleType.Pinned).AddrOfPinnedObject();
            return 1;
        }
        private void VideoCleanup(IntPtr opaque)
        {
        }

        private void RenderImage(VlcControlWpfRendererContext context)
        {
            var bitmapSource = BitmapSource.Create(context.Width, context.Height, 96, 96, context.PixelFormat, null, context.Data, context.Size, context.Stride);

            bitmapSource.SetValue(VlcRendererUtility.MemoryUsageProperty, new VlcRendererUtility.MemoryUsage(context.Size));

            VideoSource = bitmapSource;
            VideoBrush.ImageSource = VideoSource;
        }

        #endregion

        public void Dispose()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action)delegate
                    {
                        if (IsPlaying)
                            Stop();
                        LogProperties.Dispose();
                        FreeEvents();
                        VlcContext.InteropManager.MediaPlayerInterops.ReleaseInstance.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this]);
                        VlcContext.HandleManager.MediaPlayerHandles.Remove(this);

                        myVideoLockCallbackHandle.Free();
                        myVideoUnlockCallbackHandle.Free();
                        myVideoDisplayCallbackHandle.Free();

                        myVideoSetFormatHandle.Free();
                        myVideoCleanupHandle.Free();
                    });
        }

        /// <summary>
        /// Take snapshot
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <param name="width">The width of the snapshot</param>
        /// <param name="height">The height of the snapshot</param>
        public void TakeSnapshot(string filePath, uint width, uint height)
        {
            if (VlcContext.InteropManager != null &&
                VlcContext.InteropManager.MediaPlayerInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.TakeSnapshot.IsAvailable)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, 
                    (Action)(() => VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.TakeSnapshot.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], 0, filePath, width, height)));
            }
        }

    }
}
