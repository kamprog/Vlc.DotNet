using System.ComponentModel;
using Vlc.DotNet.Core.Interop.Vlc;

namespace Vlc.DotNet.Core.Medias
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class MediaOptionsBase
    {
        internal abstract void SetOptionsToMedia(string optionSeparator, VlcMedia media);
        internal abstract string[] GetOptions(string optionSeparator);
    }
}