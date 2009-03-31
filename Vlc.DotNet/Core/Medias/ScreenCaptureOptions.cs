using System.Collections.Generic;
using System.ComponentModel;
using Vlc.DotNet.Core.Interop.Vlc;

namespace Vlc.DotNet.Core.Medias
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ScreenCaptureOptions : MediaOptionsBase
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Options.ScreenCaptureOptions ScreenCapture { get; private set; }

        public ScreenCaptureOptions()
        {
            ScreenCapture = new Options.ScreenCaptureOptions();
        }
        internal override void SetOptionsToMedia(string optionSeparator, VlcMedia media)
        {
            ScreenCapture.SetOptions(optionSeparator, media, typeof(Options.ScreenCaptureOptions.ScreenOptionEnum));
        }

        internal override string[] GetOptions(string optionSeparator)
        {
            var data = new List<string>();
            data.AddRange(ScreenCapture.RetreiveOptions(optionSeparator, typeof(Options.ScreenCaptureOptions.ScreenOptionEnum)));
            return data.ToArray();
        }
    }
}