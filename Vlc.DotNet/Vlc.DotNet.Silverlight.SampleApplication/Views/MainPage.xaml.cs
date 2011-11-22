using System;
using System.Windows;
using System.Windows.Controls;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.Media;
using Vlc.DotNet.Core.Medias;
using Vlc.DotNet.Silverlight.SampleApplication.ViewModels;

namespace Vlc.DotNet.Silverlight.SampleApplication.Views
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            var vm = new MainViewModel();

            if (!VlcContext.IsInitialized)
            {
                Content = new TextBlock {Text = "Cannot initialize libvlc.", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center};
                return;
            }
            if (VlcContext.Version < new Version(1, 2))
            {
                Content = new TextBlock {Text = "Libvlc must be version 1.2.", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center};
                return;
            }

            InitializeComponent();

            myTxtUrl.Text = @"http://www.youtube.com/watch?v=7SkoknhLDEs&feature=relmfu";

            Application.Current.MainWindow.Closing +=
                delegate { OnBtnStopClick(this, null); };
        }

        private void OnBtnPlayClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(myTxtUrl.Text))
                return;
            var media = new LocationMedia(myTxtUrl.Text);
            media.StateChanged +=
                delegate(MediaBase sender2, VlcEventArgs<States> e2)
                    {
                        if (e2.Data == States.Ended && media.SubItems.Count > 0)
                        {
                            LocationMedia newMedia = media.SubItems[0];
                            //newMedia.AddOption(":file-caching=1000");
                            newMedia.ParsedChanged +=
                                delegate
                                    {
                                        myTxtTitle.Text = newMedia.Metadatas.Title;
                                        myTxtArtist.Text = newMedia.Metadatas.Artist;
                                    };
                            myVlcControl.Play(newMedia);
                        }
                    };
            media.ParsedChanged +=
                delegate { myTxtMRL.Text = media.MRL; };
            myVlcControl.Play(media);
        }

        private void OnBtnPauseClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Pause();
        }

        private void OnBtnStopClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();
        }
    }
}