namespace Vlc.DotNet.Core.Medias
{
    public sealed class ScreenCaptureMedia : MediaBase<ScreenCaptureOptions>
    {
        public ScreenCaptureMedia()
            : base("screen://")
        {
            Title = "Vlc : Screen Capture";
        }

        protected internal override string RetrieveMrl()
        {
            return Prefix;
        }
    }
}