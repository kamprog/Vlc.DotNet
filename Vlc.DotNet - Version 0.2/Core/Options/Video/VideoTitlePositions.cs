using System;

namespace Vlc.DotNet.Core.Options
{
    [Flags]
    public enum VideoTitlePositions
    {
        Center = 0,
        Left = 1,
        Right = 2,
        Top = 4,
        Bottom = 8
    }
}