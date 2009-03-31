using System.ComponentModel;
using System.IO;
using Vlc.DotNet.Core.Interop;
using Vlc.DotNet.Core.Utils;

namespace Vlc.DotNet.Core
{
    public partial class VlcManager : INotifyPropertyChanged
    {
        private bool myMute;
        private float myPosition;
        private int myVolume = 100;

        /// <summary>
        /// Get/Set autostart
        /// </summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AutoStart { get; set; }

        /// <summary>
        /// Get/Set the libvlc, libvlccore files path
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string VlcLibPath
        {
            get { return myVlcLibPath; }
            set
            {
                myVlcLibPath = value;
                if (!string.IsNullOrEmpty(value) && Directory.Exists(value) && File.Exists(Path.Combine(value, "libvlc.dll")) && File.Exists(Path.Combine(value, "libvlccore.dll")))
                {
                    TryToCreateClient();
                }
            }
        }

        /// <summary>
        /// Get/Set volume
        /// </summary>
        [DefaultValue(100)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Volume
        {
            get { return myVolume; }
            set
            {
                Throw.IfNull(value);
                if (value < 0 || value > 200)
                    Throw.Exception("Value must be between 0 and 200.");
                myVolume = value;
                if (player != null)
                    player.Audio.Volume = value;
                OnPropertyChanged("Volume");
            }
        }

        /// <summary>
        /// Mute volume
        /// </summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Mute
        {
            get { return myMute; }
            set
            {
                myMute = value;
                if (player != null)
                    player.Audio.Mute = value;
                OnPropertyChanged("Mute");
            }
        }

        /// <summary>
        /// Position of selected playing media
        /// </summary>
        [DefaultValue(0)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float Position
        {
            get { return myPosition; }
            set
            {
                Throw.IfNull(value);

                if ((int) (value*100) == (int) (myPosition*100))
                    return;

                myPosition = value;
                if (player != null && player.State == VlcState.Playing)
                    player.Position = value;
                else
                    myPosition = 0;
                OnPropertyChanged("Position");
            }
        }

        /// <summary>
        /// Get/Set fullscreen
        /// </summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool FullScreen
        {
            get
            {
                if (player != null)
                {
                    if (player.State == VlcState.Playing || player.State == VlcState.Paused)
                    {
                        return player.Video.FullScreen;
                    }
                }
                return false;
            }
            set
            {
                if (player != null && player.State == VlcState.Playing)
                {
                    player.Video.FullScreen = value;
                    OnPropertyChanged("FullScreen");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}