using System;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcPausableChangedEventArgs : VlcEventArgs
    {
        internal VlcPausableChangedEventArgs(VlcCallbackArgs Args, IntPtr UserData)
            : base(Args, UserData)
        {
        }

        public bool IsSeekable
        {
            get
            {
                return Convert.ToBoolean(Args.media_player_pausable_changed.new_pausable);
            }
        }
    }
}