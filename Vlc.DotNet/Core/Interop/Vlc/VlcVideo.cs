using System;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcVideo
    {
        internal VlcMediaPlayer _Player;

        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        internal VlcVideo(VlcMediaPlayer Player)
        {
            _Player = Player;
            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
        }

        //private VlcObject _Object;

        public IntPtr Parent
        {
            get
            {
                IntPtr rtn = InteropMethods.libvlc_media_player_get_drawable(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool FullScreen
        {
            get
            {
                bool rtn = InteropMethods.libvlc_get_fullscreen(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_set_fullscreen(_Player.p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int TeleText
        {
            get
            {
                int rtn = InteropMethods.libvlc_video_get_teletext(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_video_set_teletext(_Player.p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int SPU
        {
            get
            {
                int rtn = InteropMethods.libvlc_video_get_spu(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_video_set_spu(_Player.p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public string AspectRatio
        {
            get
            {
                string rtn = InteropMethods.libvlc_video_get_aspect_ratio(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_video_set_aspect_ratio(_Player.p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public string CropGeometry
        {
            get
            {
                string rtn = InteropMethods.libvlc_video_get_crop_geometry(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_video_set_crop_geometry(_Player.p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int Height
        {
            get
            {
                int rtn = InteropMethods.libvlc_video_get_height(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public int Width
        {
            get
            {
                int rtn = InteropMethods.libvlc_video_get_width(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public void ReParent(IntPtr parent)
        {
            InteropMethods.libvlc_video_reparent(_Player.p_media_player, parent, ref p_exception);
            p_exception.CheckException();
        }

        public void ReSize(int width, int height)
        {
            InteropMethods.libvlc_video_resize(_Player.p_media_player, width, height, ref p_exception);
            p_exception.CheckException();
        }

        public void ToggleFullScreen()
        {
            InteropMethods.libvlc_toggle_fullscreen(_Player.p_media_player, ref p_exception);
            p_exception.CheckException();
        }

        public void ToggleTeleText()
        {
            InteropMethods.libvlc_toggle_teletext(_Player.p_media_player, ref p_exception);
            p_exception.CheckException();
        }

        public void TakeSnapShot(string file, uint width, uint height)
        {
            InteropMethods.libvlc_video_take_snapshot(_Player.p_media_player, file, width, height, ref p_exception);
            p_exception.CheckException();
        }

        public void LoadSubtitleFile(string file)
        {
            InteropMethods.libvlc_video_set_subtitle_file(_Player.p_media_player, file, ref p_exception);
            p_exception.CheckException();
        }
    }
}