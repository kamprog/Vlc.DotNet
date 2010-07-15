using System;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcPositionChangedEventArgs : VlcEventArgs
    {
        internal VlcPositionChangedEventArgs(VlcCallbackArgs Args, IntPtr UserData)
            : base(Args, UserData)
        {
        }

        public float Position
        {
            get
            {
                return Args.media_player_position_changed.new_position;
            }
        }
    }
}