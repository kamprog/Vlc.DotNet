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
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public sealed partial class VlcManager : IDisposable
    {
        private VlcMediaPlayer player;
        private VideoLanClient client;
        private IntPtr handle = IntPtr.Zero;
        private string myVlcLibPath;

        public VlcManager()
        {
            MediaLibrary = new VlcMediaLibraryManager();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public VlcMediaLibraryManager MediaLibrary { get; set; }

        [DefaultValue(false)]
        public bool AutoStart { get; set; }

        public string VlcLibPath
        {
            get
            {
                return myVlcLibPath;
            }
            set
            {
                myVlcLibPath = value;
                if (!string.IsNullOrEmpty(value) &&
                    Directory.Exists(value) &&
                    File.Exists(Path.Combine(value, "libvlc.dll")) &&
                    File.Exists(Path.Combine(value, "libvlccore.dll")))
                {
                    TryToCreateClient();
                }
            }
        }

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

        #region IDisposable Members

        public void Dispose()
        {
            if (client == null)
                return;
            client.Dispose();
            client = null;
        }

        #endregion

        ~VlcManager()
        {
            Dispose();
        }

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

        public void Play(MediaBase media)
        {
            MediaLibrary.MediaItems.Clear();
            MediaLibrary.MediaItems.Add(media);
            MediaLibrary.SelectedMedia = media;
            Play();
        }

        public void Pause()
        {
            if (player != null)
                player.Pause();
        }

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

        public void Stop()
        {
            player.Stop();
            Position = 0;
        }
    }
}