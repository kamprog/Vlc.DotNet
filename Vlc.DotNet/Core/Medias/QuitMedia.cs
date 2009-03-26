namespace Vlc.DotNet.Core.Medias
{
    public sealed class QuitMedia : MediaBase
    {
        public QuitMedia()
            : base("vlc://quit")
        {
            Title = "Vlc : Quit";
        }
        protected internal override string RetrieveMrl()
        {
            return Prefix;
        }

        protected internal override string[] RetrieveOptions()
        {
            return new string[0];
        }

    }
}