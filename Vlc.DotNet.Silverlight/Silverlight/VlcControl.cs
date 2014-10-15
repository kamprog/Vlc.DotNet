using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Silverlight
{
    public partial class VlcControl : FrameworkElement
    {
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.LockCallbackDelegate myVideoLockCallback;
        private GCHandle myVideoLockCallbackHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.UnlockCallbackDelegate myVideoUnlockCallback;
        private GCHandle myVideoUnlockCallbackHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.FormatCallbackDelegate myVideoSetFormat;
        private GCHandle myVideoSetFormatHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.CleanupCallbackDelegate myVideoCleanup;
        private GCHandle myVideoCleanupHandle;
        private readonly Core.Interops.Signatures.LibVlc.MediaPlayer.Video.DisplayCallbackDelegate myDisplayCallback;
        private GCHandle myDisplayCallbackHandle;

        private WriteableBitmap myBitmap;
        private IntPtr myBitmapSectionPointer;

        private readonly object myLock = new object();

        /// <summary>
        /// Identifies the Vlc.DotNet.Wpf.VideoSource dependency property.
        /// </summary>
        public static readonly DependencyProperty VideoSourceProperty = DependencyProperty.Register("VideoSource", typeof(ImageSource), typeof(VlcControl), null);

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
        public static readonly DependencyProperty VideoBrushProperty = DependencyProperty.Register("VideoBrush", typeof(ImageBrush), typeof(VlcControl), null);

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
            if (!Application.Current.IsRunningOutOfBrowser)
                return;

            VideoBrush = new ImageBrush();

            if (!VlcContext.IsInitialized)
                VlcContext.Initialize();

            VlcContext.HandleManager.MediaPlayerHandles[this] = VlcContext.InteropManager.MediaPlayerInterops.NewInstance.Invoke(VlcContext.HandleManager.LibVlcHandle);
            AudioProperties = new VlcAudioProperties(this);
            VideoProperties = new VlcVideoProperties(this);
            LogProperties = new VlcLogProperties();
            Medias = new VlcMediaListPlayer(this);
            AudioOutputDevices = new VlcAudioOutputDevices();

            EventsHelper.ExecuteRaiseEventDelegate =
                delegate(Delegate singleInvoke, object sender, object arg)
                {
                    if (!Dispatcher.CheckAccess())
                        Dispatcher.BeginInvoke(singleInvoke, sender, arg);

                };
            InitEvents();

            myVideoLockCallback = LockCallback;
            myVideoLockCallbackHandle = GCHandle.Alloc(myVideoLockCallback);
            //myVideoUnlockCallback = UnlockCallback;
            //myVideoUnlockCallbackHandle = GCHandle.Alloc(myVideoUnlockCallback);
            myDisplayCallback = DisplayCallback;
            myDisplayCallbackHandle = GCHandle.Alloc(myDisplayCallback);

            myVideoSetFormat = VideoSetFormat;
            myVideoSetFormatHandle = GCHandle.Alloc(myVideoSetFormat);
            myVideoCleanup = VideoCleanup;
            myVideoCleanupHandle = GCHandle.Alloc(myVideoCleanup);

            CompositionTarget.Rendering += CompositionTargetRendering;

            VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetFormatCallbacks.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], myVideoSetFormat, myVideoCleanup);
            VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetCallbacks.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], myVideoLockCallback, null, myDisplayCallback, IntPtr.Zero);
        }


        #region Vlc Display

        [AllowReversePInvokeCalls]
        private void LockCallback(IntPtr opaque, ref IntPtr plane)
        {
            plane = opaque;
            //Monitor.Enter(myLock);
        }

        //private int cpt = 0;
        //[AllowReversePInvokeCalls]
        //private void UnlockCallback(IntPtr opaque, IntPtr picture, ref IntPtr planes)
        //{
        //    Monitor.Exit(myLock);
        //    Dispatcher.BeginInvoke(() =>
        //                               {
        //                                    lock(myLock)
        //                                    {
        //                                        myBitmap.Invalidate();
        //                                    }
        //                               });
        //}


        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            if (myBitmap != null)
            {
                myBitmap.Invalidate();
            }
        }

        [AllowReversePInvokeCalls]
        private uint VideoSetFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines)
        {
            var context = new VlcControlWpfRendererContext(width, height);

            chroma = BitConverter.ToUInt32(new[] { (byte)'R', (byte)'V', (byte)'3', (byte)'2' }, 0);
            width = (uint)context.Width;
            height = (uint)context.Height;
            pitches = (uint)context.Stride;
            lines = (uint)context.Height;

            myBitmapSectionPointer = Win32Interop.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, Win32Interop.PageAccess.ReadWrite, 0, context.Size, null);
            opaque = Win32Interop.MapViewOfFile(myBitmapSectionPointer, Win32Interop.FileMapAccess.AllAccess, 0, 0, (uint)context.Size);

            Dispatcher.BeginInvoke((Action)(() =>
            {
                myBitmap = new WriteableBitmap(context.Width, context.Height);
                VideoSource = myBitmap;
                VideoBrush.ImageSource = myBitmap;
            }));

            return 1;
        }

        [AllowReversePInvokeCalls]
        private void VideoCleanup(IntPtr opaque)
        {
            myBitmap = null;
            Win32Interop.UnmapViewOfFile(opaque);
            Win32Interop.CloseHandle(myBitmapSectionPointer);
        }

        [AllowReversePInvokeCalls]
        private void DisplayCallback(IntPtr opaque, IntPtr picture)
        {
            lock (myLock)
            {
                if (myBitmap == null)
                    return;
                var data = new byte[myBitmap.PixelWidth * myBitmap.PixelHeight * 32 / 8];
                Marshal.Copy(opaque, data, 0, data.Length);

                var cpt = 0;
                for (int y = 0; y < myBitmap.PixelHeight; y++)
                {
                    for (int x = 0; x < myBitmap.PixelWidth; x++)
                    {
                        SetPixel(myBitmap, x, y, data[cpt + 3], data[cpt + 2], data[cpt + 1], data[cpt]);
                        cpt += 4;
                    }                    
                }
            }
        }

        #endregion

        public void Dispose()
        {
            EventsHelper.CanRaiseEvent = false;

            CompositionTarget.Rendering -= CompositionTargetRendering;

            Dispatcher.BeginInvoke(
                new Action(
                    delegate
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

        private static void SetPixel(WriteableBitmap wb, int x, int y, byte a, byte r, byte g, byte b)
        {
            // Validate that the x,y coordinates are within the bounds of the bitmap
            //if (x < 0 || x >= wb.PixelWidth || y < 0 || y >= wb.PixelHeight) return;

            int offset = (y * wb.PixelWidth) + x;
            int pixel;
            int[] buffer = wb.Pixels;

            if (a == 255)
            {
                // Since no alpha blending is required we can directly use the incomming color
                // compose the integer that needs to be written to the pixel buffer.
                pixel = (a << 24) | (r << 16) | (g << 8) | b;
            }
            else
            {
                // Get the current pixle in the pixel buffer that we need to blend with
                pixel = buffer[offset];

                // calculate the alpha channel ratios used for the blend of 
                // the source and destination pixels
                double sourceAlpha = a / 255.0;
                double inverseSourceAlpha = 1 - sourceAlpha;

                // Extract the color components of the current pixel in the buffer
                byte destA = (byte)(pixel >> 24);
                byte destR = (byte)(pixel >> 16);
                byte destG = (byte)(pixel >> 8);
                byte destB = (byte)pixel;

                // Calculate the color components of the new pixel. 
                // This is the blend of the destination pixel and the new source pixel
                byte pixelA = (byte)((a * sourceAlpha) + (inverseSourceAlpha * destA));
                byte pixelR = (byte)((r * sourceAlpha) + (inverseSourceAlpha * destR));
                byte pixelG = (byte)((g * sourceAlpha) + (inverseSourceAlpha * destG));
                byte pixelB = (byte)((b * sourceAlpha) + (inverseSourceAlpha * destB));

                // Reconstitute the color components into an int to be written to the pixel buffer
                pixel = (pixelA << 24) | (pixelR << 16) | (pixelG << 8) | pixelB;
            }

            // Write new pixel to the pixel buffer
            buffer[offset] = pixel;
        }
    }
}
