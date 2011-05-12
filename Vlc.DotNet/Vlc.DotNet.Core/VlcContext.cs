using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Core.Medias;

namespace Vlc.DotNet.Core
{
    /// <summary>
    /// The vlc context
    /// </summary>
    public sealed class VlcContext
    {
        static VlcContext()
        {
            StartupOptions = new VlcStartupOptions();
            HandleManager = new VlcHandleManager();
            IsInitialized = false;

            if(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") == "X86")
            {
                LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_X86;
                LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_X86;
            }
            else
            {
                LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;
                LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;                
            }

        }

        internal static LibVlcInteropsManager InteropManager { get; private set; }
        internal static VlcHandleManager HandleManager { get; private set; }

        /// <summary>
        /// Get / Set the libvlc.dll and libvlccore.dll path 
        /// </summary>
        public static string LibVlcDllsPath { get; set; }

        /// <summary>
        /// Get / Set the plugins directory for vlc
        /// </summary>
        public static string LibVlcPluginsPath { get; set; }

        /// <summary>
        /// Options of the starting of Vlc
        /// </summary>
        public static VlcStartupOptions StartupOptions { get; private set; }

        /// <summary>
        /// Check if VlcContext is initialized.
        /// </summary>
        public static bool IsInitialized { get; private set; }

        internal static StringCollection GetBaseVlcInstanceArguments()
        {
            var result = new StringCollection();
            result.Add("-I");
            result.Add("dummy");
            if (StartupOptions.IgnoreConfig)
                result.Add("--ignore-config");
            result.Add("--plugin-path=" + Encoding.UTF8.GetBytes(LibVlcPluginsPath));
            if (!StartupOptions.ScreenSaverEnabled)
                result.Add("--disable-screensaver");
            if (!string.IsNullOrEmpty(StartupOptions.UserAgent))
                result.Add("--user-agent=" + Encoding.UTF8.GetBytes(StartupOptions.UserAgent));
            if (StartupOptions.LogOptions != null && StartupOptions.LogOptions.Verbosity != VlcLogOptions.Verbosities.None)
            {
                if (StartupOptions.LogOptions.ShowLoggerConsole)
                    result.Add("--extraintf=logger");
                result.Add("--verbose=" + (int) StartupOptions.LogOptions.Verbosity);
                if (StartupOptions.LogOptions.LogInFile)
                {
                    result.Add("--file-logging");
                    result.Add(@"--logfile=" + Encoding.UTF8.GetBytes(StartupOptions.LogOptions.LogInFilePath));
                }
            }
            return result;
        }

        /// <summary>
        /// Initialize library
        /// </summary>
        public static void Initialize()
        {
            InteropManager = new LibVlcInteropsManager(LibVlcDllsPath);
            if (IsInitialized)
                throw new ApplicationException("Cannot initialize more than one time.");

            var argsStringCollection = GetBaseVlcInstanceArguments();
            var args = new string[argsStringCollection.Count];
            argsStringCollection.CopyTo(args, 0);

            HandleManager.LibVlcHandle = InteropManager.NewInstance.Invoke(args.Length, args);
            if (HandleManager.LibVlcHandle != IntPtr.Zero)
                IsInitialized = true;
        }

        /// <summary>
        /// Close of LibVlc and VlcControls instance
        /// </summary>
        public static void CloseAll()
        {
            if (HandleManager != null)
            {
                var mediaBases = new MediaBase[HandleManager.MediasHandles.Count];
                HandleManager.MediasHandles.Keys.CopyTo(mediaBases, 0);
                foreach (MediaBase mediaBase in mediaBases)
                {
                    mediaBase.Dispose();
                }

                if (HandleManager.MediaPlayerHandles != null)
                {
                    var mediaPlayers = new IVlcControl[HandleManager.MediaPlayerHandles.Count];
                    HandleManager.MediaPlayerHandles.Keys.CopyTo(mediaPlayers, 0);

                    foreach (IVlcControl mediaPlayerHandle in mediaPlayers)
                    {
                        mediaPlayerHandle.Dispose();
                    }
                    HandleManager.MediaPlayerHandles.Clear();
                }
                if (InteropManager != null)
                    InteropManager.ReleaseInstance.Invoke(HandleManager.LibVlcHandle);
                HandleManager.LibVlcHandle = IntPtr.Zero;
            }
            if (InteropManager == null) 
                return;
            InteropManager.Dispose();
            InteropManager = null;
        }
    }
}