using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Medias;

namespace Vlc.DotNet.Wpf.SampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties

        /// <summary>
        /// Under position changing process
        /// </summary>
        private bool positionChanging;

        #endregion

        #region Constructor / destructor

        public MainWindow()
        {
            // Set libvlc.dll and libvlccore.dll directory path
            //VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
            // Set the vlc plugins directory path
            //VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;

            // Set the startup options
            VlcContext.StartupOptions.IgnoreConfig = true;
            VlcContext.StartupOptions.LogOptions.LogInFile = true;
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;
            VlcContext.StartupOptions.AddOption("--no-video-title-show");
            VlcContext.StartupOptions.AddOption("--play-and-pause");

            // Initialize the VlcContext
            VlcContext.Initialize();

            InitializeComponent();

            myVlcControl.VideoProperties.Scale = 2.0f;
            myVlcControl.PositionChanged += VlcControlPositionChanged;
            Closing += MainWindow_Closing;
        }

        /// <summary>
        /// Main window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            myVlcControl.Stop();
            VlcContext.CloseAll();
        }

        #endregion

        #region Control playing

        /// <summary>
        /// Play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPlayClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Play();
        }

        /// <summary>
        /// Pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Pause();
        }

        /// <summary>
        /// Stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();
            sliderPosition.Value = 0;
        }

        /// <summary>
        /// Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpenClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();

            if (myVlcControl.Media != null)
            {
                myVlcControl.Media.MediaParsedChanged -= MediaParsedChanged;
            }

            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Open media file...",
                FileName = "Media File",
                Filter = "All files |*.*"
            };

            // Process open file dialog box results
            if (dlg.ShowDialog() == true)
            {
                textBlockOpen.Visibility = Visibility.Collapsed;

                //media = new LocationMedia(@"http://88.190.232.102:6404");
                myVlcControl.Media = new PathMedia(dlg.FileName);
                myVlcControl.Media.MediaParsedChanged += MediaParsedChanged;
                myVlcControl.Play();
            }
        }

        /// <summary>
        /// Media information parsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MediaParsedChanged(MediaBase sender, VlcEventArgs<int> e)
        {
            Dispatcher.Invoke(
                DispatcherPriority.Normal,
                (Action)delegate
                {
                    textBlock.Text = string.Format(
                        "Duration: {0:00}:{1:00}:{2:00}",
                        myVlcControl.Media.Duration.Hours,
                        myVlcControl.Media.Duration.Minutes,
                        myVlcControl.Media.Duration.Seconds);

                    sliderVolume.Value = myVlcControl.AudioProperties.Volume;
                    checkboxMute.IsChecked = myVlcControl.AudioProperties.IsMute;
                });
        }

        /// <summary>
        /// Media player position changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VlcControlPositionChanged(VlcControl sender, VlcEventArgs<float> e)
        {
            Dispatcher.Invoke(
                DispatcherPriority.Normal,
                (Action)delegate
                {
                    if (!positionChanging)
                    {
                        sliderPosition.Value = e.Data;
                        textBlock.Text = string.Format(
                            "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                            myVlcControl.Time.Hours,
                            myVlcControl.Time.Minutes,
                            myVlcControl.Time.Seconds,
                            myVlcControl.Media.Duration.Hours,
                            myVlcControl.Media.Duration.Minutes,
                            myVlcControl.Media.Duration.Seconds);
                    }
                });
        }

        /// <summary>
        /// Volume value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderVolumeValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myVlcControl.AudioProperties.Volume = Convert.ToInt32(sliderVolume.Value);
        }

        /// <summary>
        /// Mute audio check changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckboxMuteCheckedChanged(object sender, RoutedEventArgs e)
        {
            myVlcControl.AudioProperties.IsMute = checkboxMute.IsChecked == true;
        }

        #endregion

        #region Change position

        /// <summary>
        /// Start position changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            positionChanging = true;
            myVlcControl.PositionChanged -= VlcControlPositionChanged;
        }

        /// <summary>
        /// Stop position changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            myVlcControl.Position = (float)sliderPosition.Value;
            myVlcControl.PositionChanged += VlcControlPositionChanged;
            positionChanging = false;
        }

        /// <summary>
        /// Change position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (positionChanging)
            {
                myVlcControl.Position = (float)sliderPosition.Value;
            }
        }

        #endregion
    }
}
