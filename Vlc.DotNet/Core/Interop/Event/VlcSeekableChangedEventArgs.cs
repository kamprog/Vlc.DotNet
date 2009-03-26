using System;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcSeekableChangedEventArgs : VlcEventArgs
    {
        internal VlcSeekableChangedEventArgs(VlcCallbackArgs Args, IntPtr UserData)
            : base(Args, UserData)
        {
        }

        public bool IsSeekable
        {
            get
            {
                return Convert.ToBoolean(Args.media_player_seekable_changed.new_seekable);
            }
        }
    }
}