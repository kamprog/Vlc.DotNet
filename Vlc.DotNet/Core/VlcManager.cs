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
        private VlcMediaPlayer player;
        private VideoLanClient client;
        private IntPtr handle = IntPtr.Zero;
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
                handle = value;
                TryToCreateClient();
            }
        }

        private void TryToCreateClient()
        {
            if (handle != IntPtr.Zero)
            {
                string path = string.IsNullOrEmpty(myVlcLibPath) ? AppDomain.CurrentDomain.BaseDirectory : myVlcLibPath;
                if (File.Exists(Path.Combine(path, "libvlc.dll")) &&
                    File.Exists(Path.Combine(path, "libvlccore.dll")))
                {
                    client = new VideoLanClient(path);
                    player = client.CreateMediaPlayer(handle);
                    player.Audio.Volume = Volume;
                    player.Audio.Mute = Mute;
                    player.PositionChanged +=
                        delegate(object sender, VlcPositionChangedEventArgs e)
                        {
                            Position = e.Position;
                        };
                    player.StateChanged +=
                        delegate(object sender, VlcStateChangedEventArgs e)
                        {
                            if (e.NewState == VlcState.Ended)
                            {
                                Position = 0;
                            }
                        };
                }
            }
        }

        /// <summary>
        /// Dispose of VlcManager
        /// </summary>
        public void Dispose()
        {
            if (client == null)
                return;
            client.Dispose();
            client = null;
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
            Throw.IfNull(player, new DirectoryNotFoundException("Vlc libraries directory not found."));

            if (MediaLibrary.MediaItems.Count > 0 && MediaLibrary.SelectedMedia == null && player.State != VlcState.Playing)
            {
                Next();
            }
            else if (MediaLibrary.MediaItems.Count > 0 && MediaLibrary.SelectedMedia != null && !MediaLibrary.MediaItems.Contains(MediaLibrary.SelectedMedia))
            {
                Next();
            }
            if (MediaLibrary.MediaItems.Count > 0 && MediaLibrary.SelectedMedia != null)
            {
                using (VlcMedia media = client.CreateMedia(MediaLibrary.SelectedMedia.RetrieveMrl()))
                {
                    foreach (string option in MediaLibrary.SelectedMedia.RetrieveOptions())
                    {
                        media.AddOption(option);
                    }
                    player.Load(media);
                }
                player.Play();
            }
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
            if (player != null)
                player.Pause();
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
            if (oldMedia != newMedia && player.State == VlcState.Playing)
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

            if (oldMedia != newMedia && player.State == VlcState.Playing)
                Play();
        }
        /// <summary>
        /// Stop playing selected media
        /// </summary>
        public void Stop()
        {
            player.Stop();
            Position = 0;
        }
        #endregion

        /// <summary>
        /// Take a snapshot of the video
        /// </summary>
        /// <param name="filepath">File path of the snapshot</param>
        public void TakeSnapShot(string filepath)
        {
            if (player != null && player.Video != null && (player.State == VlcState.Playing || player.State == VlcState.Paused))
                TakeSnapShot(filepath, player.Video.Width, player.Video.Height);
        }

        /// <summary>
        /// Take a snapshot of the video
        /// </summary>
        /// <param name="filepath">File path of the snapshot</param>
        /// <param name="width">Width of the snapshot</param>
        /// <param name="height">Height of the snapshot</param>
        public void TakeSnapShot(string filepath, int width, int height)
        {
            if (player != null && player.Video != null && (player.State == VlcState.Playing || player.State == VlcState.Paused) && width > 0 && height > 0)
                player.Video.TakeSnapShot(filepath, (uint) width, (uint) height);
        }
    }
}