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
        public MainWindow()
        {
            //Set libvlc.dll and libvlccore.dll directory path
            VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
            //Set the vlc plugins directory path
            VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;

            //Set the startup options
            VlcContext.StartupOptions.IgnoreConfig = true;
            VlcContext.StartupOptions.LogOptions.LogInFile = true;
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogOptions.Verbosities.Debug;

            //Initialize the VlcContext
            VlcContext.Initialize();

            InitializeComponent();

            //PathMedia media = new PathMedia(@"c:\test2.flv");
            LocationMedia media = new LocationMedia(@"http://88.190.232.102:6404");
            myVlcControl.SetMedia(media);
            myVlcControl.PositionChanged += vlcControl1_PositionChanged;
            myVlcControl.Play();

            Closing += MainWindow_Closing;

        }

        private void vlcControl1_PositionChanged(VlcControl sender, VlcEventArgs<float> e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action)delegate
                    {
                        Title = "Position :" + (e.Data * 100).ToString("000") + " %";
                    });
            
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            VlcContext.CloseAll();
        }
    }
}
