using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Silverlight.SampleApplication.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            /* Setting up the configuration of the VLC instance.
             * You can use any available command-line option using the AddOption function (see last two options). 
             * A list of options is available at 
             *     http://wiki.videolan.org/VLC_command-line_help
             * for example. */
            // Ignore the VLC configuration file
            VlcContext.StartupOptions.IgnoreConfig = true;
            // Disable file based logging
            VlcContext.StartupOptions.LogOptions.LogInFile = false;
            // Disable the VLC log console (in addition to the applications window)
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
            // Set the log level for the VLC instance
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;
            // Disable showing the movie file name as an overlay
            VlcContext.StartupOptions.AddOption("--no-video-title-show");
            // Pauses the playback of a movie on the last frame
            VlcContext.StartupOptions.AddOption("--play-and-pause");

            var ofd = new OpenFileDialog();
            ofd.Filter = "Libvlc Library (libvlc.dll)|*.dll|All Files|*.*";
            ofd.InitialDirectory = VlcContext.LibVlcDllsPath;
            ofd.Multiselect = false;
            while (true)
            {
                var result = ofd.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    try
                    {
                        // Set libvlc.dll and libvlccore.dll directory path
                        VlcContext.LibVlcDllsPath = ofd.File.Directory.FullName;
                        // Set the vlc plugins directory path
                        VlcContext.LibVlcPluginsPath = System.IO.Path.Combine(VlcContext.LibVlcDllsPath, "plugins");
                        // Initialize the VlcContext
                        VlcContext.Initialize();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Vlc.DotNet.silverlight.Sample", MessageBoxButton.OK);
                    }
                    if(VlcContext.IsInitialized)
                        return;
                }
                else
                {
                    return;
                }
            }

        }
    }
}
