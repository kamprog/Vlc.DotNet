using System;
using System.Collections.Generic;

namespace Vlc.DotNet.Core.Interop.Vlc.Tracks
{
    internal class VlcSubtitles : VlcTracks
    {
        private VlcMediaPlayer _Player;

        #region VlcTracks Properties

        #region internal override VlcMediaPlayer Player
        /// <summary>
        /// Gets the Player of the VlcSubtitles
        /// </summary>
        /// <value></value>
        internal override VlcMediaPlayer Player
        {
            get { return _Player; }
        }
        #endregion

        #endregion

        #region internal VlcSubtitles(VlcMediaPlayer Player)
        /// <summary>
        /// Initializes a new instance of the <b>VlcSubtitles</b> class.
        /// </summary>
        /// <param name="Player"></param>
        internal VlcSubtitles(VlcMediaPlayer Player)
        {
            _Player = Player;
            _Tracks = new Dictionary<int, VlcTrack>();
        }
        #endregion

        #region VlcSubtitles Methods

        public void LoadSubtitleFile(string file)
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_video_set_subtitle_file(_Player.p_media_player, file, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }

        #endregion

        #region VlcTracks Methods

        #region protected override int GetTrackCountInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int GetTrackCountInternal()
        {
            return InteropMethods.libvlc_video_get_spu_count(Player.p_media_player, ref Player.p_ex);
        }
        #endregion
        
        #region protected override int GetTrackCurrentInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int GetTrackCurrentInternal()
        {
            return InteropMethods.libvlc_video_get_spu(Player.p_media_player, ref Player.p_ex);
        }
        #endregion
        
        #region protected override void SetTrackCurrentInternal(int ID)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        protected override void SetTrackCurrentInternal(int ID)
        {
            InteropMethods.libvlc_video_set_spu(Player.p_media_player, ID, ref Player.p_ex);
        }
        #endregion

        #region protected override IntPtr GetTrackDescInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IntPtr GetTrackDescInternal()
        {
            return InteropMethods.libvlc_video_get_spu_description(Player.p_media_player, ref Player.p_ex);
        }
        #endregion

        #endregion
    }
}
