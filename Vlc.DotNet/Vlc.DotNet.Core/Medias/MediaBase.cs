using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.AsynchronousEvents;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.Media;

namespace Vlc.DotNet.Core.Medias
{
    /// <summary>
    /// Media abstract base class
    /// </summary>
    public abstract class MediaBase : IDisposable
    {
        private EventCallbackDelegate myEventCallback;
        private GCHandle myEventCallbackHandle;
        private IntPtr myEventManagerHandle;

        /// <summary>
        /// Retreive the duration of the media
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public TimeSpan Duration
        {
            get
            {
                if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                    return TimeSpan.Zero;
                long duration = VlcContext.InteropManager.MediaInterops.GetDuration.Invoke(VlcContext.HandleManager.MediasHandles[this]);
                if (duration == -1)
                    return TimeSpan.Zero;
                return new TimeSpan(0, 0, 0, 0, (int)duration);
            }
        }

        /// <summary>
        /// Retreive the Media Resource Locator
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string MRL
        {
            get
            {
                if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                    return null;
                return VlcContext.InteropManager.MediaInterops.GetMrl.Invoke(VlcContext.HandleManager.MediasHandles[this]);
            }
        }

        /// <summary>
        /// Retreive the current state of the media
        /// </summary>
        public States State
        {
            get
            {
                if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                    return States.NothingSpecial;
                return VlcContext.InteropManager.MediaInterops.GetState.Invoke(VlcContext.HandleManager.MediasHandles[this]);
            }
        }

        /// <summary>
        /// Retrieve the current media track informations
        /// </summary>
        public IList<MediaTrackInfo> TrackInfos
        {
            get
            {
                if (VlcContext.InteropManager.MediaInterops.IsParsed.Invoke(VlcContext.HandleManager.MediasHandles[this]) == 0)
                {
                    VlcContext.InteropManager.MediaInterops.Parse.Invoke(VlcContext.HandleManager.MediasHandles[this]);
                }

                var mediaTrackInfos = new List<MediaTrackInfo>();
                if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                {
                    return mediaTrackInfos;
                }

                IntPtr mediaInfoPtr;

                var count = VlcContext.InteropManager.MediaInterops.GetTrackInfo.Invoke(VlcContext.HandleManager.MediasHandles[this], out mediaInfoPtr);
                try
                {
                    if (count <= 0)
                        return mediaTrackInfos;

                    int size = Marshal.SizeOf(typeof(MediaTrackInfo));

                    if (mediaInfoPtr != IntPtr.Zero)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            var mediaTrackInfoItem = (MediaTrackInfo)Marshal.PtrToStructure(mediaInfoPtr, typeof(MediaTrackInfo));

                            mediaTrackInfos.Add(mediaTrackInfoItem);
                            mediaInfoPtr = new IntPtr(mediaInfoPtr.ToInt64() + size);
                        }
                    }
                }
                finally
                {
                    VlcContext.InteropManager.MediaInterops.FreeMemory.Invoke(mediaInfoPtr);
                }

                return mediaTrackInfos;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (VlcContext.HandleManager.MediasHandles.ContainsKey(this))
            {
                FreeEvents();
                VlcContext.InteropManager.MediaInterops.ReleaseInstance.Invoke(VlcContext.HandleManager.MediasHandles[this]);
            }
            VlcContext.HandleManager.MediasHandles.Remove(this);
        }

        #endregion

        protected abstract IntPtr GetNewMediaInstance();

        /// <summary>
        /// Initialize media
        /// </summary>
        protected void Initialize()
        {
            IntPtr handle = GetNewMediaInstance();
            if (handle == IntPtr.Zero)
                return;
            VlcContext.HandleManager.MediasHandles[this] = handle;
            InitEvents();
        }

        /// <summary>
        /// Add option for media
        /// </summary>
        /// <param name="option"></param>
        public void AddOption(string option)
        {
            if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                throw new Exception("Cannot set option while media is not initialized yet.");

            VlcContext.InteropManager.MediaInterops.AddOption.Invoke(VlcContext.HandleManager.MediasHandles[this], option);
        }

        #region Events

        private void InitEvents()
        {
            if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                return;
            myEventManagerHandle = VlcContext.InteropManager.MediaInterops.EventManager.Invoke(VlcContext.HandleManager.MediasHandles[this]);

            myEventCallback = OnVlcEvent;
            myEventCallbackHandle = GCHandle.Alloc(myEventCallback);

            VlcContext.InteropManager.EventInterops.Attach.Invoke(myEventManagerHandle, EventTypes.MediaDurationChanged, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Attach.Invoke(myEventManagerHandle, EventTypes.MediaFreed, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Attach.Invoke(myEventManagerHandle, EventTypes.MediaMetaChanged, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Attach.Invoke(myEventManagerHandle, EventTypes.MediaParsedChanged, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Attach.Invoke(myEventManagerHandle, EventTypes.MediaStateChanged, myEventCallback, IntPtr.Zero);
            //VlcContext.InteropManager.EventInterops.Attach.Invoke(myEventManagerHandle, EventTypes.MediaSubItemAdded, myEventCallback, IntPtr.Zero);
        }

        private void FreeEvents()
        {
            VlcContext.InteropManager.EventInterops.Detach.Invoke(myEventManagerHandle, EventTypes.MediaDurationChanged, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Detach.Invoke(myEventManagerHandle, EventTypes.MediaFreed, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Detach.Invoke(myEventManagerHandle, EventTypes.MediaMetaChanged, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Detach.Invoke(myEventManagerHandle, EventTypes.MediaParsedChanged, myEventCallback, IntPtr.Zero);
            VlcContext.InteropManager.EventInterops.Detach.Invoke(myEventManagerHandle, EventTypes.MediaStateChanged, myEventCallback, IntPtr.Zero);
            //VlcContext.InteropManager.EventInterops.Detach.Invoke(myEventManagerHandle, EventTypes.MediaSubItemAdded, myEventCallback, IntPtr.Zero);

            myEventCallbackHandle.Free();
        }

        private void OnVlcEvent(ref LibVlcEventArgs eventData, IntPtr userData)
        {
            switch (eventData.Type)
            {
                case EventTypes.MediaDurationChanged:
                    EventsHelper.RaiseEvent(MediaDurationChanged, this, new VlcEventArgs<long>(eventData.MediaDurationChanged.NewDuration));
                    break;
                case EventTypes.MediaFreed:
                    EventsHelper.RaiseEvent(MediaFreed, this, new VlcEventArgs<EventArgs>(EventArgs.Empty));
                    break;
                case EventTypes.MediaMetaChanged:
                    EventsHelper.RaiseEvent(MediaMetaChanged, this, new VlcEventArgs<Metadatas>(eventData.MediaMetaChanged.MetaType));
                    break;
                case EventTypes.MediaParsedChanged:
                    EventsHelper.RaiseEvent(MediaParsedChanged, this, new VlcEventArgs<int>(eventData.MediaParsedChanged.NewStatus));
                    break;
                case EventTypes.MediaStateChanged:
                    EventsHelper.RaiseEvent(MediaStateChanged, this, new VlcEventArgs<States>(eventData.MediaStateChanged.NewState));
                    break;
                //TODO
                //case EventTypes.MediaSubItemAdded:
                //    //eventData.MediaSubitemAdded.NewChild
                //    EventsHelper.RaiseEvent(MediaSubItemAdded, this, new VlcEventArgs<EventArgs>(EventArgs.Empty));
                //    break;
            }
        }

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<MediaBase, long> MediaDurationChanged;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<MediaBase, EventArgs> MediaFreed;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<MediaBase, Metadatas> MediaMetaChanged;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<MediaBase, int> MediaParsedChanged;

        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public event VlcEventHandler<MediaBase, States> MediaStateChanged;

        //[Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        //public event VlcEventHandler<MediaBase, EventArgs> MediaSubItemAdded;

        #endregion

        #region Metadatas

        /// <summary>
        /// Get / Set the Album metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Album
        {
            get { return GetMetadata(Metadatas.Album); }
            set { SetMetadata(Metadatas.Album, value); }
        }

        /// <summary>
        /// Get / Set the Artist metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Artist
        {
            get { return GetMetadata(Metadatas.Artist); }
            set { SetMetadata(Metadatas.Artist, value); }
        }

        /// <summary>
        /// Get / Set the ArtworkURL metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string ArtworkURL
        {
            get { return GetMetadata(Metadatas.ArtworkURL); }
            set { SetMetadata(Metadatas.ArtworkURL, value); }
        }

        /// <summary>
        /// Get / Set the Copyright metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Copyright
        {
            get { return GetMetadata(Metadatas.Copyright); }
            set { SetMetadata(Metadatas.Copyright, value); }
        }

        /// <summary>
        /// Get / Set the Date metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Date
        {
            get { return GetMetadata(Metadatas.Date); }
            set { SetMetadata(Metadatas.Date, value); }
        }

        /// <summary>
        /// Get / Set the Description metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Description
        {
            get { return GetMetadata(Metadatas.Description); }
            set { SetMetadata(Metadatas.Description, value); }
        }

        /// <summary>
        /// Get / Set the EncodedBy metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string EncodedBy
        {
            get { return GetMetadata(Metadatas.EncodedBy); }
            set { SetMetadata(Metadatas.EncodedBy, value); }
        }

        /// <summary>
        /// Get / Set the Genre metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Genre
        {
            get { return GetMetadata(Metadatas.Genre); }
            set { SetMetadata(Metadatas.Genre, value); }
        }

        /// <summary>
        /// Get / Set the Language metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Language
        {
            get { return GetMetadata(Metadatas.Language); }
            set { SetMetadata(Metadatas.Language, value); }
        }

        /// <summary>
        /// Get / Set the NowPlaying metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string NowPlaying
        {
            get { return GetMetadata(Metadatas.NowPlaying); }
            set { SetMetadata(Metadatas.NowPlaying, value); }
        }

        /// <summary>
        /// Get / Set the Publisher metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Publisher
        {
            get { return GetMetadata(Metadatas.Publisher); }
            set { SetMetadata(Metadatas.Publisher, value); }
        }

        /// <summary>
        /// Get / Set the Rating metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Rating
        {
            get { return GetMetadata(Metadatas.Rating); }
            set { SetMetadata(Metadatas.Rating, value); }
        }

        /// <summary>
        /// Get / Set the Setting metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Setting
        {
            get { return GetMetadata(Metadatas.Setting); }
            set { SetMetadata(Metadatas.Setting, value); }
        }

        /// <summary>
        /// Get / Set the Title metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string Title
        {
            get { return GetMetadata(Metadatas.Title); }
            set { SetMetadata(Metadatas.Title, value); }
        }

        /// <summary>
        /// Get / Set the TrackID metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string TrackID
        {
            get { return GetMetadata(Metadatas.TrackID); }
            set { SetMetadata(Metadatas.TrackID, value); }
        }

        /// <summary>
        /// Get / Set the TrackNumber metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string TrackNumber
        {
            get { return GetMetadata(Metadatas.TrackNumber); }
            set { SetMetadata(Metadatas.TrackNumber, value); }
        }

        /// <summary>
        /// Get / Set the URL metadata
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string URL
        {
            get { return GetMetadata(Metadatas.URL); }
            set { SetMetadata(Metadatas.URL, value); }
        }

        private string GetMetadata(Metadatas metadata)
        {
            if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                return null;
            if (VlcContext.InteropManager.MediaInterops.IsParsed.Invoke(VlcContext.HandleManager.MediasHandles[this]) == 0)
                VlcContext.InteropManager.MediaInterops.Parse.Invoke(VlcContext.HandleManager.MediasHandles[this]);
            try
            {
                return VlcContext.InteropManager.MediaInterops.GetMetadata.Invoke(VlcContext.HandleManager.MediasHandles[this], metadata);
            }
            catch
            {
                return null;
            }
        }

        private void SetMetadata(Metadatas metadata, string value)
        {
            if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                return;
            VlcContext.InteropManager.MediaInterops.SetMetadata.Invoke(VlcContext.HandleManager.MediasHandles[this], metadata, value);
        }

        /// <summary>
        /// Save the metadatas
        /// </summary>
        public void SaveMetadatas()
        {
            if (!VlcContext.HandleManager.MediasHandles.ContainsKey(this))
                return;
            VlcContext.InteropManager.MediaInterops.SaveMetadatas.Invoke(VlcContext.HandleManager.MediasHandles[this]);
        }

        #endregion
    }
}