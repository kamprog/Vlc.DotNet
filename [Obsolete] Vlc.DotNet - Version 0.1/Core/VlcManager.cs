using System;
using System.ComponentModel;
using System.IO;
using Vlc.DotNet.Core.Interop;
using Vlc.DotNet.Core.Interop.Event;
using Vlc.DotNet.Core.Interop.Vlc;
using Vlc.DotNet.Core.Medias;
using Vlc.DotNet.Core.Utils;

namespace Vlc.DotNet.Core
{
    /// <summary>
    /// VlcManager class
    /// </summary>
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public sealed partial class VlcManager : IDisposable
    {
        private VlcMediaPlayer myPlayer;
        private VideoLanClient myClient;
        private IntPtr myHandle = IntPtr.Zero;
        private string myVlcLibPath;

        /// <summary>
        /// VlcManager constructor
        /// </summary>
        public VlcManager()
        {
            MediaLibrary = new VlcMediaLibraryManager();
        }

        /// <summary>
        /// Get/Set Media libray
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public VlcMediaLibraryManager MediaLibrary { get; set; }

        [Browsable(false)]
        internal IntPtr ControlHandle
        {
            set
            {
                myHandle = value;
                TryToCreateClient();
            }
        }

        private void TryToCreateClient()
        {
            if (myHandle == IntPtr.Zero)
                return;

            string path = string.IsNullOrEmpty(myVlcLibPath) ? AppDomain.CurrentDomain.BaseDirectory : myVlcLibPath;
            if (!File.Exists(Path.Combine(path, "libvlc.dll")) || !File.Exists(Path.Combine(path, "libvlccore.dll")))
                return;
            myClient = new VideoLanClient(path);
            myClient.PluginPath = Path.Combine(path, "plugins");
            myPlayer = myClient.CreateMediaPlayer(myHandle);
            myPlayer.Audio.Volume = Volume;
            myPlayer.Audio.Mute = Mute;
            //todo
            //myPlayer.PositionChanged +=
            //    delegate(object sender, VlcPositionChangedEventArgs e)
            //    {
            //        Position = e.Position;
            //    };
            myPlayer.StateChanged +=
                delegate(object sender, VlcStateChangedEventArgs e)
                {
                    if (e.NewState == VlcState.Ended)
                    {
                        Position = 0;
                    }
                };
        }

        /// <summary>
        /// Dispose of VlcManager
        /// </summary>
        public void Dispose()
        {
            if (myClient == null)
                return;
            myClient.Dispose();
            myClient = null;
        }

        /// <summary>
        /// Destructor of VlcManager
        /// </summary>
        ~VlcManager()
        {
            Dispose();
        }

        #region MediaController
        /// <summary>
        /// Play selected media
        /// </summary>
        public void Play()
        {
            Throw.IfNull(myPlayer, new DirectoryNotFoundException("Vlc libraries directory not found."));

            if (MediaLibrary.MediaItems.Count > 0 && MediaLibrary.SelectedMedia == null && myPlayer.State != VlcState.Playing)
            {
                Next();
            }
            else if (MediaLibrary.MediaItems.Count > 0 && MediaLibrary.SelectedMedia != null && !MediaLibrary.MediaItems.Contains(MediaLibrary.SelectedMedia))
            {
                Next();
            }
            if (MediaLibrary.MediaItems.Count <= 0 || MediaLibrary.SelectedMedia == null)
                return;

            using (var media = myClient.CreateMedia(MediaLibrary.SelectedMedia.RetrieveMrl()))
            {
                foreach (var option in MediaLibrary.SelectedMedia.RetrieveOptions())
                {
                    media.AddOption(option);
                }
                myPlayer.Load(media);
            }
            myPlayer.Play();
        }
        /// <summary>
        /// Play media argument
        /// </summary>
        /// <param name="media">Media to play</param>
        public void Play(MediaBase media)
        {
            MediaLibrary.MediaItems.Clear();
            MediaLibrary.MediaItems.Add(media);
            MediaLibrary.SelectedMedia = media;
            Play();
        }
        /// <summary>
        /// Pause selected playing media
        /// </summary>
        public void Pause()
        {
            if (myPlayer != null)
                myPlayer.Pause();
        }
        /// <summary>
        /// Play previous media in the media library
        /// </summary>
        public void Previous()
        {
            if (MediaLibrary == null)
                return;
            MediaBase oldMedia = MediaLibrary.SelectedMedia;
            MediaLibrary.Previous();
            MediaBase newMedia = MediaLibrary.SelectedMedia;
            if (oldMedia != newMedia && myPlayer.State == VlcState.Playing)
                Play();
        }
        /// <summary>
        /// Play next media in media library
        /// </summary>
        public void Next()
        {
            if (MediaLibrary == null)
                return;
            MediaBase oldMedia = MediaLibrary.SelectedMedia;
            MediaLibrary.Next();
            MediaBase newMedia = MediaLibrary.SelectedMedia;

            if (oldMedia != newMedia && myPlayer.State == VlcState.Playing)
                Play();
        }
        /// <summary>
        /// Stop playing selected media
        /// </summary>
        public void Stop()
        {
            myPlayer.Stop();
            Position = 0;
        }
        #endregion

        /// <summary>
        /// Take a snapshot of the video
        /// </summary>
        /// <param name="filepath">File path of the snapshot</param>
        public void TakeSnapShot(string filepath)
        {
            if (myPlayer != null && myPlayer.Video != null && (myPlayer.State == VlcState.Playing || myPlayer.State == VlcState.Paused))
                TakeSnapShot(filepath, myPlayer.Video.Width, myPlayer.Video.Height);
        }

        /// <summary>
        /// Take a snapshot of the video
        /// </summary>
        /// <param name="filepath">File path of the snapshot</param>
        /// <param name="width">Width of the snapshot</param>
        /// <param name="height">Height of the snapshot</param>
        public void TakeSnapShot(string filepath, int width, int height)
        {
            if (myPlayer != null && myPlayer.Video != null && (myPlayer.State == VlcState.Playing || myPlayer.State == VlcState.Paused) && width > 0 && height > 0)
                myPlayer.Video.TakeSnapShot(filepath, (uint)width, (uint)height);
        }
    }
}