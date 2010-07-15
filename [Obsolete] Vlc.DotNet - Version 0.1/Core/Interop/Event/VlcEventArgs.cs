using System;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcEventArgs : EventArgs
    {
        private readonly VlcCallbackArgs _Args;
        private readonly IntPtr _UserData;

        internal VlcEventArgs(VlcCallbackArgs Args, IntPtr UserData)
        {
            _Args = Args;
            _UserData = UserData;
        }

        public IntPtr UserData
        {
            get
            {
                return _UserData;
            }
        }

        internal VlcCallbackArgs Args
        {
            get
            {
                return _Args;
            }
        }
    }
}