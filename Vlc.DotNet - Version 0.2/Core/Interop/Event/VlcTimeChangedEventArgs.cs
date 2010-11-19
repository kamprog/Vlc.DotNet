using System;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcTimeChangedEventArgs : VlcEventArgs
    {
        internal VlcTimeChangedEventArgs(VlcCallbackArgs Args, IntPtr UserData)
            : base(Args, UserData)
        {
        }

        public long Time
        {
            get
            {
                return Args.media_player_time_changed.new_time;
            }
        }
    }
}