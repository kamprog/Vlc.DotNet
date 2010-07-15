using System;
using System.Collections.Generic;

namespace Vlc.DotNet.Core.Interop.Vlc.Tracks
{
    internal class VlcVideoTracks : VlcTracks
    {
        private VlcVideo _Video;

        #region VlcTracks Properties

        #region internal override VlcMediaPlayer Player
        /// <summary>
        /// Gets the Player of the VlcVideoTracks
        /// </summary>
        /// <value></value>
        internal override VlcMediaPlayer Player
        {
            get { return _Video.Player; }
        }
        #endregion

        #endregion

        #region internal VlcVideoTracks(VlcVideo Video)
        /// <summary>
        /// Initializes a new instance of the <b>VlcVideoTracks</b> class.
        /// </summary>
        /// <param name="Video"></param>
        internal VlcVideoTracks(VlcVideo Video)
        {
            _Video = Video;
            _Tracks = new Dictionary<int, VlcTrack>();
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
            return InteropMethods.libvlc_video_get_track_count(Player.p_media_player, ref Player.p_ex);
        }
        #endregion
        
        #region protected override int GetTrackCurrentInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int GetTrackCurrentInternal()
        {
            return InteropMethods.libvlc_video_get_track(Player.p_media_player, ref Player.p_ex);
        }
        #endregion
        
        #region protected override void SetTrackCurrentInternal(int ID)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        protected override void SetTrackCurrentInternal(int ID)
        {
            InteropMethods.libvlc_video_set_track(Player.p_media_player, ID, ref Player.p_ex);
        }
        #endregion

        #region protected override IntPtr GetTrackDescInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IntPtr GetTrackDescInternal()
        {
            return InteropMethods.libvlc_video_get_track_description(Player.p_media_player, ref Player.p_ex);
        }
        #endregion

        #endregion
    }
}
