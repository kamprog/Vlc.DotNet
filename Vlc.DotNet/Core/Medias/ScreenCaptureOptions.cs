using System.Collections.Generic;
using System.ComponentModel;
using Vlc.DotNet.Core.Interop.Vlc;

namespace Vlc.DotNet.Core.Medias
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ScreenCaptureOptions : MediaOptionsBase
    {
        public Vlc.DotNet.Core.Options.ScreenCaptureOptions ScreenCapture { get; private set; }

        public ScreenCaptureOptions()
        {
            ScreenCapture = new Vlc.DotNet.Core.Options.ScreenCaptureOptions();
        }
        internal override void SetOptionsToMedia(string optionSeparator, VlcMedia media)
        {
            ScreenCapture.SetOptions(optionSeparator, media, typeof(Vlc.DotNet.Core.Options.ScreenCaptureOptions.ScreenOptionEnum));
        }

        internal override string[] GetOptions(string optionSeparator)
        {
            var data = new List<string>();
            data.AddRange(ScreenCapture.RetreiveOptions(optionSeparator, typeof(Vlc.DotNet.Core.Options.ScreenCaptureOptions.ScreenOptionEnum)));
            return data.ToArray();
        }
    }
}