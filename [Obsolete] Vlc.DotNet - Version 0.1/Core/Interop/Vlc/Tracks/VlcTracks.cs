using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interop.Vlc.Tracks
{
    internal abstract class VlcTracks : IEnumerable<VlcTrack>
    {
        internal bool _ReloadTracks = true;
        protected Dictionary<int, VlcTrack> _Tracks;

        #region virtual internal VlcMediaPlayer Player
        /// <summary>
        /// Gets the Player of the VlcTracks
        /// </summary>
        /// <value></value>
        internal abstract VlcMediaPlayer Player { get; }
        #endregion

        #region public VlcTrack this[int ID]
        /// <summary>
        /// Gets the <see cref="VlcTrack"/> item identified by the given arguments of the VlcTracks
        /// </summary>
        /// <value></value>
        public VlcTrack this[int ID]
        {
            get
            {
                if (_ReloadTracks) LoadTracks();
                if (_Tracks.ContainsKey(ID))
                    return _Tracks[ID];
                throw new Exception("Track does not exist");
            }
        }
        #endregion

        #region public VlcTrack Current
        /// <summary>
        /// Get/Sets the Current of the VlcVideoTracks
        /// </summary>
        /// <value></value>
        public VlcTrack Current
        {
            get
            {
                if (_ReloadTracks) LoadTracks();
                lock (Player.player_lock)
                {
                    int rtn = GetTrackCurrentInternal();
                    Player.p_ex.CheckException();
                    return _Tracks[rtn];
                }
            }
            set
            {
                if (_ReloadTracks) LoadTracks();
                lock (Player.player_lock)
                {
                    if (_Tracks.ContainsKey(value.ID))
                    {
                        SetTrackCurrentInternal(value.ID);
                        Player.p_ex.CheckException();
                    }
                }
            }
        }
        #endregion

        #region public int Count
        /// <summary>
        /// Gets the Count of the VlcVideoTracks
        /// </summary>
        /// <value></value>
        public int Count
        {
            get
            {
                if (_ReloadTracks) LoadTracks();
                lock (Player.player_lock)
                {
                    int rtn = GetTrackCountInternal();
                    Player.p_ex.CheckException();
                    return rtn;
                }
            }
        }
        #endregion

        protected abstract IntPtr GetTrackDescInternal();
        protected abstract int GetTrackCountInternal();
        protected abstract int GetTrackCurrentInternal();
        protected abstract void SetTrackCurrentInternal(int ID);

        #region protected void LoadTracks()
        /// <summary>
        /// 
        /// </summary>
        protected void LoadTracks()
        {
            _Tracks.Clear();
            lock (Player.player_lock)
            {
                bool done;
                IntPtr p_head = GetTrackDescInternal();
                Player.p_ex.CheckException();

                IntPtr p_track = p_head;
                do
                {
                    libvlc_track_description_t track = (libvlc_track_description_t)Marshal.PtrToStructure(p_track, typeof(libvlc_track_description_t));
                    _Tracks.Add(track.i_id, new VlcTrack(track.i_id, Marshal.PtrToStringAnsi(track.psz_name)));
                    p_track = track.p_next;
                    done = (p_track == IntPtr.Zero);

                } while (!done);
                InteropMethods.libvlc_track_description_release(p_head);
            }
            _ReloadTracks = false;
        }
        #endregion

        

        #region IEnumerable<VlcTrack> Members

        public IEnumerator<VlcTrack> GetEnumerator()
        {
            if (_ReloadTracks) LoadTracks();
            return _Tracks.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_ReloadTracks) LoadTracks();
            return _Tracks.Values.GetEnumerator();
        }

        #endregion

    }
}
