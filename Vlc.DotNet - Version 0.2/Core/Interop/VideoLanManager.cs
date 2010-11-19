using Vlc.DotNet.Core.Interop.Vlm;

namespace Vlc.DotNet.Core.Interop
{
    internal class VideoLanManager
    {
        private readonly VlmStreamCollection<VlmBroadcastStream> _Broadcasts;
        private readonly VlmStreamCollection<VlmStream> _Streams;
        private readonly VlmStreamCollection<VlmVodStream> _VODs;
        internal VideoLanClient Vlc;

        internal VideoLanManager(VideoLanClient vlc)
        {
            Vlc = vlc;
            _Streams = new VlmStreamCollection<VlmStream>();
            _Broadcasts = new VlmStreamCollection<VlmBroadcastStream>();
            _VODs = new VlmStreamCollection<VlmVodStream>();
        }

        public VlmStreamCollection<VlmStream> AllStreams
        {
            get
            {
                return _Streams;
            }
        }

        public VlmStreamCollection<VlmBroadcastStream> Broadcasts
        {
            get
            {
                return _Broadcasts;
            }
        }

        public VlmStreamCollection<VlmVodStream> VODs
        {
            get
            {
                return _VODs;
            }
        }
    }
}