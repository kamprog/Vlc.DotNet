using System;
using System.Collections.Generic;

namespace Vlc.DotNet.Core.Interop.Vlc.Tracks
{
    internal class VlcChapters : VlcTracks
    {
        private VlcMediaPlayer _Player;

        #region VlcChapters Properties

        #region public int CountForTitle
        /// <summary>
        /// Gets the chapter count for the current title
        /// </summary>
        /// <value></value>
        public int CountForTitle
        {
            get
            {
                lock (Player.player_lock)
                {
                    int rtn = InteropMethods.libvlc_media_player_get_chapter_count_for_title(Player.p_media_player, ref Player.p_ex);
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
        }
        #endregion

        #endregion

        #region VlcTracks Properties

        #region internal override VlcMediaPlayer Player
        /// <summary>
        /// Gets the Player of the VlcChapters
        /// </summary>
        /// <value></value>
        internal override VlcMediaPlayer Player
        {
            get { return _Player; }
        }
        #endregion

        #endregion

        #region internal VlcChapters(VlcMediaPlayer Player)
        /// <summary>
        /// Initializes a new instance of the <b>VlcChapters</b> class.
        /// </summary>
        /// <param name="Player"></param>
        internal VlcChapters(VlcMediaPlayer Player)
        {
            _Player = Player;
            _Tracks = new Dictionary<int, VlcTrack>();
        }
        #endregion
        
        #region VlcChapters Methods

        #region public void Next()
        /// <summary>
        /// 
        /// </summary>
        public void Next()
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_media_player_next_chapter(Player.p_media_player, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }
        #endregion

        #region public void Previous()
        /// <summary>
        /// 
        /// </summary>
        public void Previous()
        {
            lock (Player.player_lock)
            {
                InteropMethods.libvlc_media_player_previous_chapter(Player.p_media_player, ref Player.p_ex);
                Player.p_ex.CheckException();
            }
        }
        #endregion

        #endregion
        
        #region VlcTracks Methods
		
        #region protected override int GetTrackCountInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int GetTrackCountInternal()
        {
            return InteropMethods.libvlc_media_player_get_chapter_count(Player.p_media_player, ref Player.p_ex);
        }
        #endregion

        #region protected override int GetTrackCurrentInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int GetTrackCurrentInternal()
        {
            return InteropMethods.libvlc_media_player_get_chapter(Player.p_media_player, ref Player.p_ex);
        }
        #endregion

        #region protected override void SetTrackCurrentInternal(int ID)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        protected override void SetTrackCurrentInternal(int ID)
        {
            InteropMethods.libvlc_media_player_set_chapter(Player.p_media_player, ID, ref Player.p_ex);
        }
        #endregion

        #region protected override IntPtr GetTrackDescInternal()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IntPtr GetTrackDescInternal()
        {
            return InteropMethods.libvlc_video_get_chapter_description(Player.p_media_player, ref Player.p_ex);
        }
        #endregion

	    #endregion
    }
}
