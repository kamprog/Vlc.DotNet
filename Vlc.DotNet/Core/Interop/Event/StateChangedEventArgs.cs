using System;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcStateChangedEventArgs : VlcEventArgs
    {
        private readonly VlcState _NewState;

        internal VlcStateChangedEventArgs(VlcCallbackArgs Args, IntPtr UserData)
            : base(Args, UserData)
        {
            _NewState = base.Args.media_state_changed.new_state;
        }

        internal VlcStateChangedEventArgs(VlcCallbackArgs Args, IntPtr UserData, VlcState NewState)
            : base(Args, UserData)
        {
            _NewState = NewState;
        }

        public VlcState NewState
        {
            get
            {
                return _NewState;
            }
        }
    }
}