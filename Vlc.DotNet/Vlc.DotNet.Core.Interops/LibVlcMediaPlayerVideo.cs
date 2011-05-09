using System;

namespace Vlc.DotNet.Core.Interops
{
    public partial class LibVlcMediaPlayerVideo : IDisposable
    {
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetCallbacks> SetCallbacks { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetFormatCallbacks> SetFormatCallbacks { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetFormat> SetFormat { get; private set; }

        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.ToggleFullscreen> ToggleFullscreen { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetFullscreen> SetFullscreen { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.GetFullscreen> GetFullscreen { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.GetSize> GetSize { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.GetScale> GetScale { get; private set; }
        public LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetScale> SetScale { get; private set; }

        internal LibVlcMediaPlayerVideo(IntPtr libVlcDllHandle, Version vlcVersion)
        {
            SetCallbacks = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetCallbacks>(libVlcDllHandle, vlcVersion);
            SetFormatCallbacks = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetFormatCallbacks>(libVlcDllHandle, vlcVersion);
            SetFormat = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetFormat>(libVlcDllHandle, vlcVersion);

            ToggleFullscreen = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.ToggleFullscreen>(libVlcDllHandle, vlcVersion);
            SetFullscreen = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetFullscreen>(libVlcDllHandle, vlcVersion);
            GetFullscreen = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.GetFullscreen>(libVlcDllHandle, vlcVersion);
            GetSize = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.GetSize>(libVlcDllHandle, vlcVersion);
            GetScale = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.GetScale>(libVlcDllHandle, vlcVersion);
            SetScale = new LibVlcFunction<Signatures.LibVlc.MediaPlayer.Video.SetScale>(libVlcDllHandle, vlcVersion);
        }

        public void Dispose()
        {
            SetCallbacks = null;
            SetFormatCallbacks = null;
            SetFormat = null;

            ToggleFullscreen = null;
            SetFullscreen = null;
            GetFullscreen = null;
            GetSize = null;
            GetScale = null;
            SetScale = null;
        }
    }
}