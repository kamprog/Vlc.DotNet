using System;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.MediaPlayer.Audio;

namespace Vlc.DotNet.Core.Interops
{
    public partial class LibVlcMediaPlayerAudio : IDisposable
    {
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.ToggleMute> ToggleMute { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.GetMute> GetMute { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.SetMute> SetMute { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.GetVolume> GetVolume { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.SetVolume> SetVolume { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.GetTrackCount> GetTrackCount { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.GetTrack> GetTrack { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.SetTrack> SetTrack { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.GetChannel> GetChannel { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.SetChannel> SetChannel { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.GetDelay> GetDelay { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Audio.SetDelay> SetDelay { get; private set; }

        internal LibVlcMediaPlayerAudio(IntPtr libVlcDllHandle, Version vlcVersion)
        {
            ToggleMute = new LibVlcFunction<ToggleMute>(libVlcDllHandle, vlcVersion);
            GetMute = new LibVlcFunction<GetMute>(libVlcDllHandle, vlcVersion);
            SetMute = new LibVlcFunction<SetMute>(libVlcDllHandle, vlcVersion);
            GetVolume = new LibVlcFunction<GetVolume>(libVlcDllHandle, vlcVersion);
            SetVolume = new LibVlcFunction<SetVolume>(libVlcDllHandle, vlcVersion);
            GetTrackCount = new LibVlcFunction<GetTrackCount>(libVlcDllHandle, vlcVersion);
            GetTrack = new LibVlcFunction<GetTrack>(libVlcDllHandle, vlcVersion);
            SetTrack = new LibVlcFunction<SetTrack>(libVlcDllHandle, vlcVersion);
            GetChannel = new LibVlcFunction<GetChannel>(libVlcDllHandle, vlcVersion);
            SetChannel = new LibVlcFunction<SetChannel>(libVlcDllHandle, vlcVersion);
            GetDelay = new LibVlcFunction<GetDelay>(libVlcDllHandle, vlcVersion);
            SetDelay = new LibVlcFunction<SetDelay>(libVlcDllHandle, vlcVersion);
        }

        public void Dispose()
        {
            ToggleMute = null;
            GetMute = null;
            SetMute = null;
            GetVolume = null;
            SetVolume = null;
            GetTrackCount = null;
            GetTrack = null;
            SetTrack = null;
            GetChannel = null;
            SetChannel = null;
            GetDelay = null;
            SetDelay = null;
        }
    }
}