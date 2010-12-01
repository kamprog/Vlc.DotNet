using Vlc.DotNet.Core.Interop;

namespace Vlc.DotNet.Core.Medias
{
    public sealed class MediaTrackInfo
    {
        internal MediaTrackInfo(libvlc_media_track_info_t info)
        {
            Codec = info.i_codec;
            Id = info.i_id;
            TrackType = (MediaTrackType) info.i_type;
            Profile = info.i_profile;
            Level = info.i_level;
            AudioChannels = info.audio.i_channels;
            AudioRate = info.audio.i_rate;
            VideoHeight = info.video.i_height;
            VideoWidth = info.video.i_width;
        }

        public uint Codec { get; private set; }
        public int Id { get; private set; }
        public MediaTrackType TrackType { get; private set; }
        public int Profile { get; private set; }
        public int Level { get; private set; }
        public uint AudioChannels { get; private set; }
        public uint AudioRate { get; private set; }
        public uint VideoHeight { get; private set; }
        public uint VideoWidth { get; private set; }
    }
}