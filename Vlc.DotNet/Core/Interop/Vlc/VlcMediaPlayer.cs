using System;
using Vlc.DotNet.Core.Interop.Event;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcMediaPlayer : IDisposable
    {
        #region Events

        //private static bool D3DImageLoaded = false;
        public event EventHandler<VlcStateChangedEventArgs> StateChanged;
        public event EventHandler<VlcTimeChangedEventArgs> TimeChanged;
        public event EventHandler<VlcPositionChangedEventArgs> PositionChanged;
        public event EventHandler<VlcSeekableChangedEventArgs> SeekableChanged;
        public event EventHandler<VlcPausableChangedEventArgs> PausableChanged;

        #endregion

        private readonly VlcAudio _Audio;
        private readonly VlcVideo _Video;
        private VlcEventManager _EventManager;
        internal VideoLanClient _Vlc;
        private VlcCallback cbPausableChanged;

        //Callback delegates
        private VlcCallback cbPositionChanged;
        private VlcCallback cbSeekableChanged;
        private VlcCallback cbStateChanged;
        private VlcCallback cbTimeChanged;

        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        internal libvlc_exception_t p_exception;

        /// <summary>
        /// COM pointer to our vlc media instance
        /// </summary>
        internal IntPtr p_media_player;

        #region Properties

        public long Length
        {
            get
            {
                long rtn = InteropMethods.libvlc_media_player_get_length(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public long Time
        {
            get
            {
                long rtn = InteropMethods.libvlc_media_player_get_time(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_media_player_set_time(p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public float Position
        {
            get
            {
                float rtn = InteropMethods.libvlc_media_player_get_position(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_media_player_set_position(p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int Chapter
        {
            get
            {
                int rtn = InteropMethods.libvlc_media_player_get_chapter(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_media_player_set_chapter(p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public int ChapterCount
        {
            get
            {
                int rtn = InteropMethods.libvlc_media_player_get_chapter_count(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public float Rate
        {
            get
            {
                float rtn = InteropMethods.libvlc_media_player_get_rate(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
            set
            {
                InteropMethods.libvlc_media_player_set_rate(p_media_player, value, ref p_exception);
                p_exception.CheckException();
            }
        }

        public virtual VlcState State
        {
            get
            {
                VlcState rtn = InteropMethods.libvlc_media_player_get_state(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public float FPS
        {
            get
            {
                float rtn = InteropMethods.libvlc_media_player_get_fps(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool WillPlay
        {
            get
            {
                bool rtn = InteropMethods.libvlc_media_player_will_play(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool CanSeek
        {
            get
            {
                bool rtn = InteropMethods.libvlc_media_player_is_seekable(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool CanPause
        {
            get
            {
                bool rtn = InteropMethods.libvlc_media_player_can_pause(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool HasVout
        {
            get
            {
                bool rtn = InteropMethods.libvlc_media_player_has_vout(p_media_player, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public VlcVideo Video
        {
            get
            {
                return _Video;
            }
        }

        public VlcAudio Audio
        {
            get
            {
                return _Audio;
            }
        }

        public IntPtr Surface
        {
            get
            {
                return IntPtr.Zero;
            }
        }

        #endregion

        //private static bool d3dlibLoaded = false;

        internal VlcMediaPlayer(VideoLanClient Vlc, IntPtr p_media_player)
        {
            _Vlc = Vlc;

            //set our Media instance pointer
            this.p_media_player = p_media_player;
            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
            _Audio = new VlcAudio(this);
            _Video = new VlcVideo(this);

            InitalizeEvents();
        }

        ~VlcMediaPlayer()
        {
            Dispose(false);
        }

        #region IDisposable Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // release managed code
            if (disposing)
            {
            }
            //release unmanaged code
            if (p_media_player != IntPtr.Zero)
            {
                Stop();
                InteropMethods.libvlc_media_player_release(p_media_player);
            }
            p_media_player = IntPtr.Zero;
        }

        #endregion

        #region Player Control

        public virtual void Play()
        {
            SyncEvents();
            InteropMethods.libvlc_media_player_play(p_media_player, ref p_exception);
            p_exception.CheckException();
        }

        public virtual void Pause()
        {
            SyncEvents();
            InteropMethods.libvlc_media_player_pause(p_media_player, ref p_exception);
            p_exception.CheckException();
        }

        public virtual void Stop()
        {
            SyncEvents();
            InteropMethods.libvlc_media_player_stop(p_media_player, ref p_exception);
            p_exception.CheckException();
        }

        public virtual void Load(VlcMedia Media)
        {
            SyncEvents();
            InteropMethods.libvlc_media_player_set_media(p_media_player, Media.p_media, ref p_exception);
            p_exception.CheckException();
        }

        #endregion

        #region Event Initalization/Synchronization

        protected void InitalizeEvents()
        {
            //Get our event manager for this class
            IntPtr p_event_manager = InteropMethods.libvlc_media_player_event_manager(p_media_player, ref p_exception);
            p_exception.CheckException();
            _EventManager = new VlcEventManager(p_event_manager);

            //Initalize our callback methods
            cbTimeChanged = OnTimeChanged;
            cbStateChanged = OnStateChanged;
            cbPositionChanged = OnPositionChanged;
            cbPausableChanged = OnPausableChanged;
            cbSeekableChanged = OnSeekableChanged;

            //Attach default events 
            _EventManager.AttachEvent(VlcEventType.MediaPlayerNothingSpecial, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerOpening, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerBuffering, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerPlaying, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerPaused, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerStopped, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerForward, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerBackward, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerEndReached, cbStateChanged, IntPtr.Zero);
            _EventManager.AttachEvent(VlcEventType.MediaPlayerEncounteredError, cbStateChanged, IntPtr.Zero);
        }

        public void SyncEvents()
        {
            SyncTimeChangedEvent();
            SyncPositionChangedEvent();
            SyncSeekableChangedEvent();
            SyncPausableChangedEvent();
        }

        private void SyncTimeChangedEvent()
        {
            if (cbTimeChanged != null)
            {
                if (TimeChanged != null && !_EventManager.IsAttached(VlcEventType.MediaPlayerTimeChanged))
                {
                    //User is listening to event, but we are not recieving callbacks from vlc
                    _EventManager.AttachEvent(VlcEventType.MediaPlayerTimeChanged, cbTimeChanged, IntPtr.Zero);
                }
                else if (TimeChanged == null && _EventManager.IsAttached(VlcEventType.MediaPlayerTimeChanged))
                {
                    //User is not listening to event, but we are still recieving callbacks from vlc
                    _EventManager.DetachEvent(VlcEventType.MediaPlayerTimeChanged, cbTimeChanged, IntPtr.Zero);
                }
            }
            else
            {
                throw new NullReferenceException("cbTimeChanged should never be null.");
            }
        }

        private void SyncPositionChangedEvent()
        {
            if (cbPositionChanged != null)
            {
                if (PositionChanged != null && !_EventManager.IsAttached(VlcEventType.MediaPlayerPositionChanged))
                {
                    //User is listening to event, but we are not recieving callbacks from vlc
                    _EventManager.AttachEvent(VlcEventType.MediaPlayerPositionChanged, cbPositionChanged, IntPtr.Zero);
                }
                else if (PositionChanged == null && _EventManager.IsAttached(VlcEventType.MediaPlayerPositionChanged))
                {
                    //User is not listening to event, but we are still recieving callbacks from vlc
                    _EventManager.DetachEvent(VlcEventType.MediaPlayerPositionChanged, cbPositionChanged, IntPtr.Zero);
                }
            }
            else
            {
                throw new NullReferenceException("cbPositionChanged should never be null.");
            }
        }

        private void SyncSeekableChangedEvent()
        {
            if (cbSeekableChanged != null)
            {
                if (SeekableChanged != null && !_EventManager.IsAttached(VlcEventType.MediaPlayerSeekableChanged))
                {
                    //User is listening to event, but we are not recieving callbacks from vlc
                    _EventManager.AttachEvent(VlcEventType.MediaPlayerSeekableChanged, cbSeekableChanged, IntPtr.Zero);
                }
                else if (SeekableChanged == null && _EventManager.IsAttached(VlcEventType.MediaPlayerSeekableChanged))
                {
                    //User is not listening to event, but we are still recieving callbacks from vlc
                    _EventManager.DetachEvent(VlcEventType.MediaPlayerSeekableChanged, cbSeekableChanged, IntPtr.Zero);
                }
            }
            else
            {
                throw new NullReferenceException("cbSeekableChanged should never be null.");
            }
        }

        private void SyncPausableChangedEvent()
        {
            if (cbPausableChanged != null)
            {
                if (PausableChanged != null && !_EventManager.IsAttached(VlcEventType.MediaPlayerPausableChanged))
                {
                    //User is listening to event, but we are not recieving callbacks from vlc
                    _EventManager.AttachEvent(VlcEventType.MediaPlayerPausableChanged, cbPausableChanged, IntPtr.Zero);
                }
                else if (PausableChanged == null && _EventManager.IsAttached(VlcEventType.MediaPlayerPausableChanged))
                {
                    //User is not listening to event, but we are still recieving callbacks from vlc
                    _EventManager.DetachEvent(VlcEventType.MediaPlayerPausableChanged, cbPausableChanged, IntPtr.Zero);
                }
            }
            else
            {
                throw new NullReferenceException("cbPausableChanged should never be null.");
            }
        }

        #endregion

        #region Events/Callbacks from VLC

        private void OnTimeChanged(ref VlcCallbackArgs Args, IntPtr UserData)
        {
            if (TimeChanged != null)
            {
                TimeChanged(this, new VlcTimeChangedEventArgs(Args, UserData));
            }
            else if (cbTimeChanged != null && _EventManager.IsAttached(VlcEventType.MediaPlayerTimeChanged))
            {
                //No one is listening to this event... Remove it to save resources.
                _EventManager.DetachEvent(VlcEventType.MediaPlayerTimeChanged, cbTimeChanged, IntPtr.Zero);
            }
        }

        private void OnPositionChanged(ref VlcCallbackArgs Args, IntPtr UserData)
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, new VlcPositionChangedEventArgs(Args, UserData));
            }
            else if (cbPositionChanged != null && _EventManager.IsAttached(VlcEventType.MediaPlayerPositionChanged))
            {
                //No one is listening to this event... Remove it to save resources.
                _EventManager.DetachEvent(VlcEventType.MediaPlayerPositionChanged, cbPositionChanged, IntPtr.Zero);
            }
        }

        private void OnSeekableChanged(ref VlcCallbackArgs Args, IntPtr UserData)
        {
            if (SeekableChanged != null)
            {
                SeekableChanged(this, new VlcSeekableChangedEventArgs(Args, UserData));
            }
            else if (cbTimeChanged != null && _EventManager.IsAttached(VlcEventType.MediaPlayerSeekableChanged))
            {
                //No one is listening to this event... Remove it to save resources.
                _EventManager.DetachEvent(VlcEventType.MediaPlayerSeekableChanged, cbSeekableChanged, IntPtr.Zero);
            }
        }

        private void OnPausableChanged(ref VlcCallbackArgs Args, IntPtr UserData)
        {
            if (PausableChanged != null)
            {
                PausableChanged(this, new VlcPausableChangedEventArgs(Args, UserData));
            }
            else if (cbPausableChanged != null && _EventManager.IsAttached(VlcEventType.MediaPlayerPausableChanged))
            {
                //No one is listening to this event... Remove it to save resources.
                _EventManager.DetachEvent(VlcEventType.MediaPlayerPausableChanged, cbPausableChanged, IntPtr.Zero);
            }
        }

        private void OnStateChanged(ref VlcCallbackArgs Args, IntPtr UserData)
        {
            if (StateChanged != null)
            {
                switch (Args.EventType)
                {
                    case VlcEventType.MediaPlayerNothingSpecial:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.NothingSpecial));
                        break;
                    case VlcEventType.MediaPlayerOpening:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Opening));
                        break;
                    case VlcEventType.MediaPlayerBuffering:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Buffering));
                        break;
                    case VlcEventType.MediaPlayerPlaying:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Playing));
                        break;
                    case VlcEventType.MediaPlayerPaused:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Paused));
                        break;
                    case VlcEventType.MediaPlayerStopped:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Stopped));
                        break;
                    case VlcEventType.MediaPlayerForward:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Forward));
                        break;
                    case VlcEventType.MediaPlayerBackward:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Backward));
                        break;
                    case VlcEventType.MediaPlayerEndReached:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Ended));
                        break;
                    case VlcEventType.MediaPlayerEncounteredError:
                        StateChanged(this, new VlcStateChangedEventArgs(Args, UserData, VlcState.Error));
                        break;
                }
            }
        }

        #endregion
    }
}