using System;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcMediaListPlayer : VlcMediaPlayer
    {
        private VlcMediaList _PlayList;

        /// <summary>
        /// COM pointer to our vlc media instance
        /// </summary>
        internal IntPtr p_ml_player;


        public VlcMediaList PlayList
        {
            get
            {
                return _PlayList;
            }
        }

        public override VlcState State
        {
            get
            {
                VlcState rtn = InteropMethods.libvlc_media_list_player_get_state(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        #region Constructors/Destructors

        #region internal VlcMediaListPlayer(VideoLanClient Vlc, IntPtr p_ml_player, IntPtr p_media_player)

        /// <summary>
        /// Initializes a new instance of the <b>VlcMediaListPlayer</b> class.
        /// </summary>
        /// <param name="Vlc"></param>
        /// <param name="p_ml_player"></param>
        /// <param name="p_media_player"></param>
        internal VlcMediaListPlayer(VideoLanClient Vlc, VlcMediaList List, IntPtr p_ml_player, IntPtr p_media_player)
            : base(Vlc, p_media_player)
        {
            //set our Media instance pointer
            this.p_ml_player = p_ml_player;

            InteropMethods.libvlc_media_list_player_set_media_player(p_ml_player, p_media_player, ref p_exception);
            p_exception.CheckException();

            Load(List);
        }

        #endregion

        #region ~VlcMediaListPlayer()

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before 
        /// the <b>VlcMediaListPlayer</b> is reclaimed by garbage collection.
        /// </summary>
        ~VlcMediaListPlayer()
        {
            Dispose(false);
        }

        #endregion

        #endregion

        #region IDisposable Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // release managed code
            if (disposing)
            {
            }
            //release unmanaged code
            if (p_ml_player != IntPtr.Zero)
            {
                InteropMethods.libvlc_media_list_player_release(p_ml_player);
            }
            p_media_player = IntPtr.Zero;
        }

        #endregion

        #region Player Control

        #region public override void Play()

        /// <summary>
        /// Play from the playlists current location
        /// </summary>
        public override void Play()
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_play(p_ml_player, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region public void Play(int Index)

        /// <summary>
        /// Play the file at the specified index
        /// </summary>
        /// <param name="Index"></param>
        public void Play(int Index)
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_play_item_at_index(p_ml_player, Index, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region public void Play(VlcMedia Media)

        /// <summary>
        /// Play the specified media from the playlist
        /// </summary>
        /// <param name="Media"></param>
        public void Play(VlcMedia Media)
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_play_item(p_ml_player, Media.p_media, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region public void Next()

        /// <summary>
        /// 
        /// </summary>
        public void Next()
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_next(p_ml_player, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region public override void Pause()

        /// <summary>
        /// 
        /// </summary>
        public override void Pause()
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_pause(p_ml_player, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region public override void Stop()

        /// <summary>
        /// 
        /// </summary>
        public override void Stop()
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_stop(p_ml_player, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region public override void Load(VlcMedia Media)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Media"></param>
        public override void Load(VlcMedia Media)
        {
            VlcMediaList lst = _Vlc.CreateMediaList();
            lst.Add(Media);
            Load(lst);
        }

        #endregion

        #region public void Load(VlcMediaList List)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="List"></param>
        public void Load(VlcMediaList List)
        {
            SyncEvents();
            InteropMethods.libvlc_media_list_player_set_media_list(p_ml_player, List.p_mlist, ref p_exception);
            p_exception.CheckException();
            _PlayList = List;
        }

        #endregion

        #endregion
    }
}