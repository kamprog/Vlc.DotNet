using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Medias
{
    public sealed class FileMedia : MediaBase<FileOptions>
    {
        private string myFilePath;

        public string FilePath
        {
            get
            {
                return myFilePath;
            }
            set
            {
                myFilePath = value;
                Title = value;
            }
        }

        public FileMedia()
            : base("file://{0}")
        {
        }

        public FileMedia(string filePath)
            : this()
        {
            FilePath = filePath;
            Title = filePath;
        }

        protected internal override string RetrieveMrl()
        {
            return Prefix.FormatIt(FilePath);
        }
    }
}