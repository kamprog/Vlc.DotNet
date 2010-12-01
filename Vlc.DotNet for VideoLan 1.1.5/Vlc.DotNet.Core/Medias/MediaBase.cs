using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Helpers;
using Vlc.DotNet.Core.Interop;

namespace Vlc.DotNet.Core.Medias
{
    public abstract class MediaBase : IDisposable
    {
        private IntPtr myEventCallback;
        private bool myIsDisposed;
        private IntPtr myVlcMediaPlayerEventManager;

        #region Meta Properties

        public string Album
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Album));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Album, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Artist
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Artist));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Artist, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string ArtworkUrl
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_ArtworkURL));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_ArtworkURL, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Copyright
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Copyright));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Copyright, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Date
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Date));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Date, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Description
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Description));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Description, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string EncodedBy
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_EncodedBy));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_EncodedBy, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Genre
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Genre));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Genre, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Language
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Language));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Language, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string NowPlaying
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_NowPlaying));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_NowPlaying, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Publisher
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Publisher));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Publisher, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Rating
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Rating));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Rating, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Setting
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Setting));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Setting, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Title
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Title));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_Title, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string TrackId
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_TrackID));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_TrackID, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string TrackNumber
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_TrackNumber));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_TrackNumber, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        public string Url
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_meta(VlcMedia, libvlc_meta_t.libvlc_meta_URL));
                return null;
            }
            set
            {
                if (IsAttachedToControl)
                    LibVlcMethods.libvlc_media_set_meta(VlcMedia, libvlc_meta_t.libvlc_meta_URL, value);
                throw new MediaNotAttachedToVlcControl();
            }
        }

        #endregion

        protected MediaBase()
        {
            Statistic = new MediaStatistic(this);
        }

        internal IntPtr VlcMedia { get; set; }

        public long? Duration
        {
            get
            {
                if (IsAttachedToControl)
                    return LibVlcMethods.libvlc_media_get_duration(VlcMedia);
                return null;
            }
        }

        public string Mrl
        {
            get
            {
                if (IsAttachedToControl)
                    return IntPtrExtensions.ToStringAnsi(LibVlcMethods.libvlc_media_get_mrl(VlcMedia));
                return null;
            }
        }

        public State State
        {
            get
            {
                if (IsAttachedToControl)
                    return (State) LibVlcMethods.libvlc_media_get_state(VlcMedia);
                return State.NothingSpecial;
            }
        }

        public bool IsParsed
        {
            get
            {
                if (IsAttachedToControl)
                    return LibVlcMethods.libvlc_media_is_parsed(VlcMedia);
                return false;
            }
        }

        public MediaStatistic Statistic { get; private set; }

        public MediaTrackInfo[] TrackInfo
        {
            get
            {
                if (VlcMedia == IntPtr.Zero)
                    return new MediaTrackInfo[0];
                var infos = new libvlc_media_track_info_t[1];
                var cpt = LibVlcMethods.libvlc_media_get_tracks_info(VlcMedia, infos);
                var result = new MediaTrackInfo[infos.Length];
                for (var index = 0; index < cpt; index++)
                {
                    var info = infos[index];
                    result[index] = new MediaTrackInfo(info);
                }
                return result;
            }
        }

        public bool IsAttachedToControl
        {
            get { return VlcMedia != IntPtr.Zero; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (myIsDisposed)
                return;

            if (myVlcMediaPlayerEventManager != IntPtr.Zero)
            {
                LibVlcMethods.libvlc_event_detach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaMetaChanged, myEventCallback, IntPtr.Zero);
                LibVlcMethods.libvlc_event_detach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaSubItemAdded, myEventCallback, IntPtr.Zero);
                LibVlcMethods.libvlc_event_detach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaDurationChanged, myEventCallback, IntPtr.Zero);
                LibVlcMethods.libvlc_event_detach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaParsedChanged, myEventCallback, IntPtr.Zero);
                LibVlcMethods.libvlc_event_detach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaFreed, myEventCallback, IntPtr.Zero);
                LibVlcMethods.libvlc_event_detach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaStateChanged, myEventCallback, IntPtr.Zero);
            }
            myIsDisposed = true;
        }

        #endregion

        protected internal virtual IntPtr Initialize(IntPtr vlcClient)
        {
            if (VlcMedia == IntPtr.Zero)
                return IntPtr.Zero;
            LibVlcMethods.EventCallbackDelegate callback = OnVlcEvent;
            myEventCallback = Marshal.GetFunctionPointerForDelegate(callback);
            GC.KeepAlive(callback);

            myVlcMediaPlayerEventManager = LibVlcMethods.libvlc_media_event_manager(VlcMedia);

            LibVlcMethods.libvlc_event_attach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaMetaChanged, myEventCallback, IntPtr.Zero);
            LibVlcMethods.libvlc_event_attach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaSubItemAdded, myEventCallback, IntPtr.Zero);
            LibVlcMethods.libvlc_event_attach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaDurationChanged, myEventCallback, IntPtr.Zero);
            LibVlcMethods.libvlc_event_attach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaParsedChanged, myEventCallback, IntPtr.Zero);
            LibVlcMethods.libvlc_event_attach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaFreed, myEventCallback, IntPtr.Zero);
            LibVlcMethods.libvlc_event_attach(myVlcMediaPlayerEventManager, libvlc_event_e.MediaStateChanged, myEventCallback, IntPtr.Zero);
            return VlcMedia;
        }

        public void ParseMeta()
        {
            if (IsAttachedToControl)
                LibVlcMethods.libvlc_media_parse(VlcMedia);
            else
                throw new MediaNotAttachedToVlcControl();
        }

        public void ParseMetaAsynch()
        {
            if (IsAttachedToControl)
                LibVlcMethods.libvlc_media_parse_async(VlcMedia);
            else
                throw new MediaNotAttachedToVlcControl();
        }

        public void SaveMeta()
        {
            if (IsAttachedToControl)
                LibVlcMethods.libvlc_media_save_meta(VlcMedia);
            else
                throw new MediaNotAttachedToVlcControl();
        }

        #region Events

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<MetaData> MetaChanged;

        //TODO
        //[Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        //public event VlcEventHandler<MediaBase> SubItemAdded;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<long> DurationChanged;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<int> ParsedChanged;

        //TODO
        //[Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        //public event VlcEventHandler<MediaBase> Freed;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<State> StateChanged;

        private void OnVlcEvent(ref libvlc_event_t type, IntPtr userdata)
        {
            switch (type.type)
            {
                case libvlc_event_e.MediaMetaChanged:
                    EventsHelper.RaiseEvent(MetaChanged, this, new VlcEventArgs<MetaData>((MetaData) type.media_meta_changed.meta_type));
                    break;
                case libvlc_event_e.MediaSubItemAdded:
                    //TODO
                    //EventsHelper.RaiseEvent(SubItemAdded, this, new VlcEventArgs<MetaData>((MetaData)type.media_subitem_added.new_child));
                    break;
                case libvlc_event_e.MediaDurationChanged:
                    EventsHelper.RaiseEvent(DurationChanged, this, new VlcEventArgs<long>(type.media_duration_changed.new_duration));
                    break;
                case libvlc_event_e.MediaParsedChanged:
                    EventsHelper.RaiseEvent(ParsedChanged, this, new VlcEventArgs<int>(type.media_parsed_changed.new_status));
                    break;
                case libvlc_event_e.MediaFreed:
                    //TODO
                    //EventsHelper.RaiseEvent(Freed, this, new VlcEventArgs<MediaBase>(type.media_freed.md));
                    break;
                case libvlc_event_e.MediaStateChanged:
                    EventsHelper.RaiseEvent(StateChanged, this, new VlcEventArgs<State>((State) type.media_state_changed.new_state));
                    break;
            }
        }

        #endregion
    }
}