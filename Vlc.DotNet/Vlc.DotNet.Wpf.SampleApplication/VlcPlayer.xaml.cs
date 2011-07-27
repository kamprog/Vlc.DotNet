// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VlcPlayer.xaml.cs" company="VideoLan DotNet">
//   See http://vlcdotnet.codeplex.com/license
// </copyright>
// <summary>
//   Example application for WPF
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Medias;

namespace Vlc.DotNet.Wpf.SampleApplication
{
    /// <summary>
    /// Interaction logic for VlcPlayer.xaml
    /// </summary>
    /// <remarks>
    /// Remember that the WPF example requires VLC 1.2 nightly or later. You might have to update the LibVlcDllsPath
    /// and LibVlcPluginsPath settings in the constructor to match your installation. 
    /// </remarks>
    public partial class VlcPlayer : Window
    {
        #region Properties

        /// <summary>
        /// Used to indicate that the user is currently changing the position (and the position bar shall not be updated). 
        /// </summary>
        private bool positionChanging;

        #endregion

        #region Constructor / destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VlcPlayer"/> class.
        /// </summary>
        public VlcPlayer()
        {
            // Set libvlc.dll and libvlccore.dll directory path
            VlcContext.LibVlcDllsPath = @"C:\Projets\vlc-1.2.0-git-20110723-0002";

            // Set the vlc plugins directory path
            VlcContext.LibVlcPluginsPath = @"C:\Projets\vlc-1.2.0-git-20110723-0002\plugins";

            /* Setting up the configuration of the VLC instance.
             * You can use any available command-line option using the AddOption function (see last two options). 
             * A list of options is available at 
             *     http://wiki.videolan.org/VLC_command-line_help
             * for example. */

            // Ignore the VLC configuration file
            VlcContext.StartupOptions.IgnoreConfig = true;

            // Enable file based logging
            VlcContext.StartupOptions.LogOptions.LogInFile = true;

            // Shows the VLC log console (in addition to the applications window)
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;

            // Set the log level for the VLC instance
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;

            // Disable showing the movie file name as an overlay
            VlcContext.StartupOptions.AddOption("--no-video-title-show");

            // Pauses the playback of a movie on the last frame
            VlcContext.StartupOptions.AddOption("--play-and-pause");

            // Initialize the VlcContext
            VlcContext.Initialize();

            InitializeComponent();

            myVlcControl.VideoProperties.Scale = 2.0f;
            myVlcControl.PositionChanged += VlcControlOnPositionChanged;
            myVlcControl.TimeChanged += VlcControlOnTimeChanged;
            Closing += MainWindowOnClosing;
        }

        /// <summary>
        /// Main window closing event
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void MainWindowOnClosing(object sender, CancelEventArgs e)
        {
            // Close the context. 
            VlcContext.CloseAll();
        }

        #endregion

        #region Control playing

        /// <summary>
        /// Called if the Play button is clicked; starts the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPlayClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Play();
        }

        /// <summary>
        /// Called if the Pause button is clicked; pauses the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Pause();
        }

        /// <summary>
        /// Called if the Stop button is clicked; stops the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();
            sliderPosition.Value = 0;
        }

        /// <summary>
        /// Called if the Open button is clicked; shows the open file dialog to select a media file to play. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonOpenClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();

            if (myVlcControl.Media != null)
            {
                myVlcControl.Media.ParsedChanged -= MediaOnParsedChanged;
            }

            var openFileDialog = new OpenFileDialog
                                     {
                                         Title = "Open media file for playback",
                                         FileName = "Media File",
                                         Filter = "All files |*.*"
                                     };

            // Process open file dialog box results
            if (openFileDialog.ShowDialog() != true)
                return;

            textBlockOpen.Visibility = Visibility.Collapsed;

            myVlcControl.Media = new PathMedia(openFileDialog.FileName);
            myVlcControl.Media.ParsedChanged += MediaOnParsedChanged;
            myVlcControl.Play();

            /* Instead of opening a file for playback you can also connect to media streams using
             *     myVlcControl.Media = new LocationMedia(@"http://88.190.232.102:6404");
             *     myVlcControl.Play();
             */
        }

        /// <summary>
        /// Volume value changed by the user. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderVolumeValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myVlcControl.AudioProperties.Volume = Convert.ToInt32(sliderVolume.Value);
        }

        /// <summary>
        /// Mute audio check changed
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void CheckboxMuteCheckedChanged(object sender, RoutedEventArgs e)
        {
            myVlcControl.AudioProperties.IsMute = checkboxMute.IsChecked == true;
        }

        #endregion

        /// <summary>
        /// Called by <see cref="VlcControl.Media"/> when the media information was parsed. 
        /// </summary>
        /// <param name="sender">Event sending media. </param>
        /// <param name="e">VLC event arguments. </param>
        private void MediaOnParsedChanged(MediaBase sender, VlcEventArgs<int> e)
        {
            textBlock.Text = string.Format(
                "Duration: {0:00}:{1:00}:{2:00}",
                myVlcControl.Media.Duration.Hours,
                myVlcControl.Media.Duration.Minutes,
                myVlcControl.Media.Duration.Seconds);

            sliderVolume.Value = myVlcControl.AudioProperties.Volume;
            checkboxMute.IsChecked = myVlcControl.AudioProperties.IsMute;
        }

        /// <summary>
        /// Called by the <see cref="VlcControl"/> when the media position changed during playback.
        /// </summary>
        /// <param name="sender">Event sennding control. </param>
        /// <param name="e">VLC event arguments. </param>
        private void VlcControlOnPositionChanged(VlcControl sender, VlcEventArgs<float> e)
        {
            if (positionChanging)
            {
                // User is currently changing the position using the slider, so do not update. 
                return;
            }

            sliderPosition.Value = e.Data;
        }

        private void VlcControlOnTimeChanged(VlcControl sender, VlcEventArgs<TimeSpan> e)
        {
            textBlock.Text = string.Format(
                "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                e.Data.Hours,
                e.Data.Minutes,
                e.Data.Seconds,
                myVlcControl.Media.Duration.Hours,
                myVlcControl.Media.Duration.Minutes,
                myVlcControl.Media.Duration.Seconds);
        }

        #region Change position

        /// <summary>
        /// Start position changing, prevents updates for the slider by the player. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            positionChanging = true;
            myVlcControl.PositionChanged -= VlcControlOnPositionChanged;
        }

        /// <summary>
        /// Stop position changing, re-enables updates for the slider by the player. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myVlcControl.Position = (float)sliderPosition.Value;
            myVlcControl.PositionChanged += VlcControlOnPositionChanged;
            
            positionChanging = false;
        }

        /// <summary>
        /// Change position when the slider value is updated. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (positionChanging)
            {
                myVlcControl.Position = (float)sliderPosition.Value;
            }
            //Update the current position text when it is in pause
            var duration = myVlcControl.Media.Duration;
            var time = TimeSpan.FromMilliseconds(duration.TotalMilliseconds * myVlcControl.Position);
            textBlock.Text = string.Format(
                "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                time.Hours,
                time.Minutes,
                time.Seconds,
                duration.Hours,
                duration.Minutes,
                duration.Seconds);
        }

        #endregion
    }
}