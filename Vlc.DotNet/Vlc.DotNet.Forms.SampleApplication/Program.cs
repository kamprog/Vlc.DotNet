// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="VideoLan DotNet">
//   See http://vlcdotnet.codeplex.com/license
// </copyright>
// <summary>
//   Example application for Windows Forms
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vlc.DotNet.Forms.SampleApplication
{
    using System;
    using System.Windows.Forms;

    using Vlc.DotNet.Core;

    /// <summary>
    /// Application entry point and VlcContext initialization
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set libvlc.dll and libvlccore.dll directory path
            VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;

            // Set the vlc plugins directory path
            VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;

            // Ignore the VLC configuration file
            VlcContext.StartupOptions.IgnoreConfig = true;

            // Enable file based logging
            VlcContext.StartupOptions.LogOptions.LogInFile = true;

            // Shows the VLC log console (in addition to the applications window)
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;

            // Set the log level for the VLC instance
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;

            // Initialize the VlcContext
            VlcContext.Initialize();

            Application.Run(new VlcPlayer());

            // Close the VlcContext
            VlcContext.CloseAll();
        }
    }
}
