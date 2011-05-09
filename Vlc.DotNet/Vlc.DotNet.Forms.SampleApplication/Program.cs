using System;
using System.Windows.Forms;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Forms.SampleApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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

            Application.Run(new Form1());

            //Close the VlcContext
            VlcContext.CloseAll();
        }
    }
}
