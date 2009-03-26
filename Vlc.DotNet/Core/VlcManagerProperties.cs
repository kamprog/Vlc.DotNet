using System;
using System.ComponentModel;
using Vlc.DotNet.Core.Utils;
using Vlc.DotNet.Core.Interop;

namespace Vlc.DotNet.Core
{
    public partial class VlcManager : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private int myVolume = 100;
        private bool myMute = false;
        private float myPosition = 0;

        [DefaultValue(100)]
        public int Volume
        {
            get
            {
                return myVolume;
            }
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
        [DefaultValue(false)]
        public bool Mute
        {
            get
            {
                return myMute;
            }
            set
            {
                myMute = value;
                if (player != null)
                    player.Audio.Mute = value;
                OnPropertyChanged("Mute");
            }
        }
        [DefaultValue(0)]
        public float Position
        {
            get
            {
                return myPosition;
            }
            set
            {
                Throw.IfNull(value);

                if ((int)(value * 100) == (int)(myPosition * 100))
                    return;

                myPosition = value;
                if (player != null && player.State == VlcState.Playing)
                    player.Position = value;
                else
                    myPosition = 0;
                OnPropertyChanged("Position");
            }
        }
        [DefaultValue(false)]
        public bool FullScreen
        {
            get
            {
                if (player != null)
                {
                    if (player.State == VlcState.Playing ||
                       player.State == VlcState.Paused)
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
    }
}
