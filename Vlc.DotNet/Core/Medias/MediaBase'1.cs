using System.ComponentModel;
using System.Windows.Markup;

namespace Vlc.DotNet.Core.Medias
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ContentProperty("Options")]
    public abstract class MediaBase<T> : MediaBase
        where T : MediaOptionsBase, new()
    {
        protected MediaBase(string prefix)
            : base(prefix)
        {
            Options = new T();
        }

        public T Options { get; set; }

        protected internal override string[] RetrieveOptions()
        {
            return Options.GetOptions(":");
        }
    }
}