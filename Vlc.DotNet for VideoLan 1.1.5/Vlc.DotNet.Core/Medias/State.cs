namespace Vlc.DotNet.Core.Medias
{
    public enum State
    {
        NothingSpecial = 0,
        Opening,
        Buffering,
        Playing,
        Paused,
        Stopped,
        Ended,
        Error
    }
}