namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcAudio
    {
        internal VlcMediaPlayer _Player;

        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        internal VlcAudio(VlcMediaPlayer Player)
        {
            _Player = Player;
            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
        }


        public int Channel
        {
            get
            {
                int rtn = InteropMethods.libvlc_audio_get_channel(_Player._Vlc.p_instance, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_audio_set_channel(_Player._Vlc.p_instance, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public bool Mute
        {
            get
            {
                bool rtn = InteropMethods.libvlc_audio_get_mute(_Player._Vlc.p_instance, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_audio_set_mute(_Player._Vlc.p_instance, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int Track
        {
            get
            {
                int rtn = InteropMethods.libvlc_audio_get_track(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_audio_set_track(_Player.p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int TrackCount
        {
            get
            {
                int rtn = InteropMethods.libvlc_audio_get_track_count(_Player.p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public int Volume
        {
            get
            {
                int rtn = InteropMethods.libvlc_audio_get_volume(_Player._Vlc.p_instance, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_audio_set_volume(_Player._Vlc.p_instance, value, ref p_exception);
                p_exception.CheckException();
            }
        }


        public void ToggleMute()
        {
            InteropMethods.libvlc_audio_toggle_mute(_Player._Vlc.p_instance, ref p_exception);
            p_exception.CheckException();
        }
    }
}