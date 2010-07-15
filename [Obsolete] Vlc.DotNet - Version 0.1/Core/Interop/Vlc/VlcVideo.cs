using System;
using Vlc.DotNet.Core.Interop.Vlc.Tracks;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcVideo
    {
        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private VlcMediaPlayer _Player;

        private VlcVideoTracks _Tracks;



        internal VlcMediaPlayer Player
        {
            get { return _Player; }
        }

        public VlcVideoTracks Tracks
        {
            get { return _Tracks; }
        }

        public IntPtr Parent
        {
            get
            {
                lock (Player.player_lock)
                {
                    IntPtr rtn = InteropMethods.libvlc_media_player_get_hwnd(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
        }
        public bool FullScreen
        {
            get
            {
                lock (Player.player_lock)
                {
                    bool rtn = InteropMethods.libvlc_get_fullscreen(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_set_fullscreen(_Player.p_media_player, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }
        public int TeleText
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_video_get_teletext(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_video_set_teletext(_Player.p_media_player, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }

        public string AspectRatio
        {
            get
            {
                lock (Player.player_lock)
                {
                    string rtn = InteropMethods.libvlc_video_get_aspect_ratio(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_video_set_aspect_ratio(_Player.p_media_player, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }

        public float Scale
        {
            get
            {
                lock (Player.player_lock)
                {
                    float rtn = InteropMethods.libvlc_video_get_scale(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_video_set_scale(_Player.p_media_player, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }

        public string CropGeometry
        {
            get
            {
                lock (Player.player_lock)
                {
                    string rtn = InteropMethods.libvlc_video_get_crop_geometry(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_video_set_crop_geometry(_Player.p_media_player, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }
        public int Height
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_video_get_height(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
        }
        public int Width
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_video_get_width(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
        }

        internal VlcVideo(VlcMediaPlayer Player)
        {
            _Player = Player;

            _Tracks = new VlcVideoTracks(this);
        }

        ////Deprecated?????????
        //public void ReParent(IntPtr parent)
        //{
        //    lock (Player.player_lock)
        //    {
        //        InteropMethods.libvlc_video_reparent(_Player.p_media_player, parent, ref Player.p_ex);
        //        Player.p_ex.CheckException();
        //    }
        //}
        //public void ReSize(int width, int height)
        //{
        //    lock (Player.player_lock)
        //    {
        //        InteropMethods.libvlc_video_resize(_Player.p_media_player, width, height, ref Player.p_ex);
        //        Player.p_ex.CheckException();
        //    }
        //}

        public void ToggleFullScreen()
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_toggle_fullscreen(_Player.p_media_player, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }
        public void ToggleTeleText()
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_toggle_teletext(_Player.p_media_player, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }
        public void TakeSnapShot(string file, uint width, uint height)
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_video_take_snapshot(_Player.p_media_player, file, width, height, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }
    }
}