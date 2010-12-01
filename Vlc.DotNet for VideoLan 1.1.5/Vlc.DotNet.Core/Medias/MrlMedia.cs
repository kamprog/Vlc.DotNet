using System;
using Vlc.DotNet.Core.Interop;

namespace Vlc.DotNet.Core.Medias
{
    public class MrlMedia : MediaBase
    {
        private bool myIsInitialized;

        public string Mrl { get; set; }

        protected internal override IntPtr Initialize(IntPtr vlcClient)
        {
            if (myIsInitialized || string.IsNullOrEmpty(Mrl))
                return IntPtr.Zero;
            VlcMedia = LibVlcMethods.libvlc_media_new_location(vlcClient, Mrl);
            if (VlcMedia == IntPtr.Zero)
            {
                throw new NotImplementedException();
            }

            base.Initialize(vlcClient);

            myIsInitialized = true;
            return VlcMedia;
        }
    }
}