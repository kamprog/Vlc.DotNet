using System.Collections.Generic;
using System.ComponentModel;
using Vlc.DotNet.Core.Interop.Vlc;
using Vlc.DotNet.Core.Options;

namespace Vlc.DotNet.Core.Medias
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class FileOptions : MediaOptionsBase
    {
        public FileOptions()
        {
            Audio = new AudioOptions();
            Video = new VideoOptions();
        }

        public AudioOptions Audio { get; set; }
        public VideoOptions Video { get; set; }

        internal override void SetOptionsToMedia(string optionSeparator, VlcMedia media)
        {
            Audio.SetOptions(optionSeparator, media, typeof (AudioOptions.AudioOptionEnum));
            Video.SetOptions(optionSeparator, media, typeof (VideoOptions.VideoOptionEnum));
        }

        internal override string[] GetOptions(string optionSeparator)
        {
            var data = new List<string>();
            data.AddRange(Audio.RetreiveOptions(optionSeparator, typeof (AudioOptions.AudioOptionEnum)));
            data.AddRange(Video.RetreiveOptions(optionSeparator, typeof (VideoOptions.VideoOptionEnum)));
            return data.ToArray();
        }
    }
}