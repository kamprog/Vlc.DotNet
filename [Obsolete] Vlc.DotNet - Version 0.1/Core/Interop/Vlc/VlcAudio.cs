using Vlc.DotNet.Core.Interop.Vlc.Tracks;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcAudio
    {
        private VlcMediaPlayer _Player;
        internal VlcMediaPlayer Player
        {
            get { return _Player; }
        }

        private VlcAudioTracks _Tracks;


        public AudioOutputChannel Channel
        {
            get
            {
                lock (Player.player_lock)
                {
                    AudioOutputChannel rtn = InteropMethods.libvlc_audio_get_channel(_Player._Vlc.p_instance, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_audio_set_channel(_Player._Vlc.p_instance, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }
        public bool Mute
        {
            get
            {
                lock (Player.player_lock)
                {
                    bool rtn = InteropMethods.libvlc_audio_get_mute(_Player._Vlc.p_instance, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_audio_set_mute(_Player._Vlc.p_instance, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }
        public int Track
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_audio_get_track(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_audio_set_track(_Player.p_media_player, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }
        public int TrackCount
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_audio_get_track_count(_Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
        }
        public int Volume
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_audio_get_volume(_Player._Vlc.p_instance, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
            set
            {
                lock (Player.player_lock)
                {
                    InteropMethods.libvlc_audio_set_volume(_Player._Vlc.p_instance, value, ref Player.p_ex);
                    Player.p_ex.CheckException();
                }
            }
        }
        public VlcAudioTracks Tracks
        {
            get { return _Tracks; }
        }


        internal VlcAudio(VlcMediaPlayer Player)
        {
            _Player = Player;
            _Tracks = new VlcAudioTracks(this);
        }

        public void ToggleMute()
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_audio_toggle_mute(_Player._Vlc.p_instance, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }

    }
}