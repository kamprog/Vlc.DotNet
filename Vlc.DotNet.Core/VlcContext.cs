using System;
using System.Collections.Specialized;
using System.Threading;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Core.Medias;

namespace Vlc.DotNet.Core
{
    /// <summary>
    /// The vlc context.
    /// </summary>
    public static class VlcContext
    {
        static VlcContext()
        {
            StartupOptions = new VlcStartupOptions();
            HandleManager = new VlcHandleManager();
            IsInitialized = false;
#if !SILVERLIGHT
            var processorArchitecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            var processorArchiteW6432 = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432");

            if ((!string.IsNullOrEmpty(processorArchitecture) && processorArchitecture.ToUpper() == "AMD64") ||
                (!string.IsNullOrEmpty(processorArchiteW6432) && processorArchiteW6432.ToUpper() == "AMD64"))
            {
                LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;
                LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
            }
            else
            {
#endif
                LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_X86;
                LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_X86;
 #if !SILVERLIGHT
           }
#endif
        }

        public static LibVlcInteropsManager InteropManager { get; private set; }
        internal static VlcHandleManager HandleManager { get; private set; }

        /// <summary>
        /// Vlc error handling.
        /// </summary>
        public static VlcErrorHandling ErrorHandling { get; private set; }

        /// <summary>
        /// Gets or Sets the LibVlcDLL paths.
        /// </summary>
        public static string LibVlcDllsPath { get; set; }

        /// <summary>
        /// Gets or Sets the plugin directory for the VLC.
        /// </summary>
        public static string LibVlcPluginsPath { get; set; }

        /// <summary>
        /// Gets the vlc startup options.
        /// </summary>
        public static VlcStartupOptions StartupOptions { get; private set; }

        /// <summary>
        /// Checks if VlcContext is initialized.
        /// </summary>
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets the VLC version.
        /// </summary>
        public static Version Version
        {
            get
            {
                if (!IsInitialized)
                    throw new ApplicationException("VlcContext must be initailized before getting his version.");
                return InteropManager != null ? InteropManager.VlcVersion : null;
            }
        }

        /// <summary>
        /// Gets the VLC compiler.
        /// </summary>
        public static string Compiler
        {
            get
            {
                if (!IsInitialized)
                    throw new ApplicationException("VlcContext must be initailized before getting his compiler.");
                return InteropManager != null ? IntPtrExtensions.ToStringAnsi(InteropManager.GetCompiler.Invoke()) : null;
            }
        }

        /// <summary>
        /// Gets the VLC ChangeSet.
        /// </summary>
        public static string ChangeSet
        {
            get
            {
                if (!IsInitialized)
                    throw new ApplicationException("Vlc must be initailized before getting his change set.");
                return InteropManager != null ? IntPtrExtensions.ToStringAnsi(InteropManager.GetChangeSet.Invoke()) : null;
            }
        }

        internal static StringCollection GetBaseVlcInstanceArguments()
        {
            var result = new StringCollection();
            result.Add("-I");
            result.Add("dummy");
            //result.Add("--no-snapshot-preview");
            if (StartupOptions.IgnoreConfig)
                result.Add("--ignore-config");
            result.Add("--plugin-path=" + LibVlcPluginsPath);
            if (!StartupOptions.ScreenSaverEnabled)
                result.Add("--disable-screensaver");
            if (!string.IsNullOrEmpty(StartupOptions.UserAgent))
                result.Add("--user-agent=" + StartupOptions.UserAgent);
            if (StartupOptions.LogOptions != null && StartupOptions.LogOptions.Verbosity != VlcLogVerbosities.None)
            {
                if (StartupOptions.LogOptions.ShowLoggerConsole)
                    result.Add("--extraintf=logger");
                result.Add("--verbose=" + (int)StartupOptions.LogOptions.Verbosity);
                if (StartupOptions.LogOptions.LogInFile)
                {
                    result.Add("--file-logging");
                    result.Add(@"--logfile=" + StartupOptions.LogOptions.LogInFilePath);
                }
            }
            foreach (var vlcStartupOption in StartupOptions.Options)
            {
                if (!result.Contains(vlcStartupOption))
                    result.Add(vlcStartupOption);
            }

            return result;
        }

        /// <summary>
        /// Initialize library
        /// </summary>
        public static IntPtr Initialize()
        {
            InteropManager = new LibVlcInteropsManager(LibVlcDllsPath);
            if (IsInitialized)
                throw new ApplicationException("Cannot initialize libvlc more than one time.");
            var argsStringfCollection = GetBaseVlcInstanceArguments();
            var args = new string[argsStringfCollection.Count];
            argsStringfCollection.CopyTo(args, 0);
            HandleManager.LibVlcHandle = InteropManager.NewInstance.Invoke(args.Length, args);
            if (HandleManager.LibVlcHandle != IntPtr.Zero)
            {
                IsInitialized = true;
                ErrorHandling = new VlcErrorHandling();
                EventsHelper.CanRaiseEvent = true;
            }
            return HandleManager.LibVlcHandle;
        }

        /// <summary>
        /// Closes all usages of LibVlc and VlcContext.
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
            ErrorHandling = null;
            if (InteropManager == null)
                return;
            InteropManager.Dispose();
            InteropManager = null;
            IsInitialized = false;
        }
    }
}