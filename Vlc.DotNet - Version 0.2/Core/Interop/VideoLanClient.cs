using System;
using System.IO;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interop.Vlc;

namespace Vlc.DotNet.Core.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void D3dCallback(IntPtr Handle, IntPtr Surface);

    internal class VideoLanClient : IDisposable
    {
        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        /// <summary>
        /// COM pointer to our vlc instance
        /// </summary>
        internal IntPtr p_instance;

        //private D3dCallback cbSurfaceChanged;
        //private readonly Dictionary<IntPtr, IntPtr> SurfacePtrs;

        internal string PluginPath;
        internal string RootPath;

        public VideoLanClient()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] args = {
                                path,
                                ":no-one-instance",
                                ":no-loop",
                                ":no-drop-late-frames",
                                ":disable-screensaver",
                                ":vout=d3dimage",
                                "--plugin-path=" + Path.Combine(path, "plugins")
                            };
            Initalize(path, args);
        }

        public VideoLanClient(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Initalize(path, args);
        }

        public VideoLanClient(string vlcDirectory)
        {
            string[] args = {
                                vlcDirectory,
                                ":no-one-instance",
                                ":no-loop",
                                ":no-drop-late-frames",
                                ":disable-screensaver",
                                //":vout=d3dimage",
                                //":vout=direct3d",
                                "--plugin-path=" + Path.Combine(vlcDirectory, "plugins")
                            };
            Initalize(vlcDirectory, args);
        }

        public VideoLanClient(string vlcDirectory, string[] args)
        {
            //SurfacePtrs = new Dictionary<IntPtr, IntPtr>();
            Initalize(vlcDirectory, args);
        }

        ~VideoLanClient()
        {
            Dispose(false);
        }

        private void Initalize(string vlcDirectory, string[] args)
        {
            RootPath = vlcDirectory;

            //save the original EnviornmentDirectory.
            string originalDir = Environment.CurrentDirectory;
            //Set the directory to load the COM object libvlc.dll
            Environment.CurrentDirectory = vlcDirectory;

            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
            p_instance = InteropMethods.libvlc_new(args.Length, args, ref p_exception);
            p_exception.CheckException();
            Environment.CurrentDirectory = originalDir;
        }

        public VlcMedia CreateMedia(string MRL)
        {
            IntPtr p_media = InteropMethods.libvlc_media_new(p_instance, MRL, ref p_exception);
            p_exception.CheckException();
            return new VlcMedia(p_media);
        }

        public VlcMediaList CreateMediaList()
        {
            IntPtr p_mlist = InteropMethods.libvlc_media_list_new(p_instance, ref p_exception);
            p_exception.CheckException();
            return new VlcMediaList(this, p_mlist);
        }

        public VlcMediaPlayer CreateMediaPlayer()
        {
            IntPtr p_media_player = InteropMethods.libvlc_media_player_new(p_instance, ref p_exception);
            p_exception.CheckException();
            return new VlcMediaPlayer(this, p_media_player);
        }

        public VlcMediaPlayer CreateMediaPlayer(IntPtr parent)
        {
            IntPtr p_media_player = InteropMethods.libvlc_media_player_new(p_instance, ref p_exception);
            p_exception.CheckException();
            //InteropMethods.libvlc_media_player_set_nsobject(p_media_player, parent, ref p_exception);
            InteropMethods.libvlc_media_player_set_hwnd(p_media_player, parent, ref p_exception);
            p_exception.CheckException();
            return new VlcMediaPlayer(this, p_media_player);
        }

        public VlcMediaPlayer CreateMediaPlayer(IntPtr parent, VlcMedia media)
        {
            IntPtr p_media_player = InteropMethods.libvlc_media_player_new_from_media(media.p_media, ref p_exception);
            p_exception.CheckException();
            //InteropMethods.libvlc_media_player_set_nsobject(p_media_player, parent, ref p_exception);
            InteropMethods.libvlc_media_player_set_hwnd(p_media_player, parent, ref p_exception);
            p_exception.CheckException();
            return new VlcMediaPlayer(this, p_media_player);
        }

        public void SetControlToMediaPlayer(IntPtr p_media_player, IntPtr parent)
        {
            //InteropMethods.libvlc_media_player_set_nsobject(p_media_player, parent, ref p_exception);
            InteropMethods.libvlc_media_player_set_hwnd(p_media_player, parent, ref p_exception);
            p_exception.CheckException();
        }


        public VlcMediaListPlayer CreateMediaListPlayer(IntPtr parent)
        {
            return CreateMediaListPlayer(parent, CreateMediaList());
        }

        public VlcMediaListPlayer CreateMediaListPlayer(IntPtr parent, VlcMediaList playlist)
        {
            IntPtr p_media_player = InteropMethods.libvlc_media_player_new(p_instance, ref p_exception);
            p_exception.CheckException();
            //InteropMethods.libvlc_media_player_set_nsobject(p_media_player, parent, ref p_exception);
            InteropMethods.libvlc_media_player_set_hwnd(p_media_player, parent, ref p_exception);
            p_exception.CheckException();
            IntPtr p_ml_player = InteropMethods.libvlc_media_list_player_new(p_instance, ref p_exception);
            p_exception.CheckException();
            return new VlcMediaListPlayer(this, playlist, p_ml_player, p_media_player);
        }

        #region IDisposable Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            // release managed code
            if (disposing)
            {
            }
            if (p_instance != IntPtr.Zero)
            {
                InteropMethods.libvlc_release(p_instance);
            }
            p_instance = IntPtr.Zero;
        }

        #endregion
    }
}