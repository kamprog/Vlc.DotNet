using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Wpf
{
    /// <summary>
    /// VlcControl class
    /// </summary>
    public sealed partial class VlcControl : FrameworkElement
    {
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.LockCallbackDelegate myVideoLockCallback;
        private GCHandle myVideoLockCallbackHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.FormatCallbackDelegate myVideoSetFormat;
        private GCHandle myVideoSetFormatHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.CleanupCallbackDelegate myVideoCleanup;
        private GCHandle myVideoCleanupHandle;

        private InteropBitmap myBitmap;
        private IntPtr myBitmapSectionPointer;

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
            AudioOutputDevices = new VlcAudioOutputDevices();
            InitEvents();

            myVideoLockCallback = LockCallback;
            myVideoLockCallbackHandle = GCHandle.Alloc(myVideoLockCallback);

            myVideoSetFormat = VideoSetFormat;
            myVideoSetFormatHandle = GCHandle.Alloc(myVideoSetFormat);
            myVideoCleanup = VideoCleanup;
            myVideoCleanupHandle = GCHandle.Alloc(myVideoCleanup);

            CompositionTarget.Rendering += CompositionTargetRendering;

            VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetFormatCallbacks.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], myVideoSetFormat, myVideoCleanup);
            VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetCallbacks.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], myVideoLockCallback, null, null, IntPtr.Zero);
        }

        #region Vlc Display

        private void LockCallback(IntPtr opaque, ref IntPtr plane)
        {
            plane = opaque;
        }

        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            if (myBitmap != null) { myBitmap.Invalidate(); }
        }

        private uint VideoSetFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines)
        {
            var context = new VlcControlWpfRendererContext(width, height, PixelFormats.Bgr32);

            chroma = BitConverter.ToUInt32(new[] { (byte)'R', (byte)'V', (byte)'3', (byte)'2' }, 0);
            width = (uint)context.Width;
            height = (uint)context.Height;
            pitches = (uint)context.Stride;
            lines = (uint)context.Height;

            myBitmapSectionPointer = Win32Interop.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, Win32Interop.PageAccess.ReadWrite, 0, context.Size, null);
            opaque = Win32Interop.MapViewOfFile(myBitmapSectionPointer, Win32Interop.FileMapAccess.AllAccess, 0, 0, (uint)context.Size);

            Dispatcher.Invoke((Action)(() =>
            {
                myBitmap = (InteropBitmap)Imaging.CreateBitmapSourceFromMemorySection(myBitmapSectionPointer, context.Width, context.Height, context.PixelFormat, context.Stride, 0);
                VideoSource = myBitmap;
                VideoBrush.ImageSource = myBitmap;
            }));

            return 1;
        }

        private void VideoCleanup(IntPtr opaque)
        {
            myBitmap = null;
            Win32Interop.UnmapViewOfFile(opaque);
            Win32Interop.CloseHandle(myBitmapSectionPointer);
        }

        #endregion

        public void Dispose()
        {
            CompositionTarget.Rendering -= CompositionTargetRendering;
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                FreeEvents();
                if (IsPlaying)
                    Stop();
                AudioProperties.Dispose();
                VideoProperties.Dispose();
                LogProperties.Dispose();
                AudioOutputDevices.Dispose();
                VlcContext.InteropManager.MediaPlayerInterops.ReleaseInstance.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this]);
                VlcContext.HandleManager.MediaPlayerHandles.Remove(this);

                myVideoLockCallbackHandle.Free();

                myVideoSetFormatHandle.Free();
                myVideoCleanupHandle.Free();
            }));
        }
    }
}
