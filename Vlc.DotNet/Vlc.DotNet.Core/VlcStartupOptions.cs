using System.ComponentModel;

namespace Vlc.DotNet.Core
{
    /// <summary>
    /// VlcStartupOptions class
    /// </summary>
    public sealed class VlcStartupOptions
    {
        /// <summary>
        /// Constructor of VlcStartupOptions
        /// </summary>
        public VlcStartupOptions()
        {
            IgnoreConfig = false;
            ScreenSaverEnabled = false;
            UserAgent = "";
            LogOptions = new VlcLogOptions();
            ShowLoggerConsole = false;
        }

        /// <summary>
        /// Get / Set ignore config
        /// </summary>
        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public bool IgnoreConfig { get; set; }

        /// <summary>
        /// Get / Set enable screensaver
        /// </summary>
        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public bool ScreenSaverEnabled { get; set; }

        /// <summary>
        /// Get / Set the startup user agent
        /// </summary>
        [DefaultValue("")]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string UserAgent { get; set; }

        /// <summary>
        /// Get / Set the startup log options
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public VlcLogOptions LogOptions { get; private set; }

        /// <summary>
        /// Get / Set the show of log console
        /// </summary>
        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public bool ShowLoggerConsole { get; set; }
    }
}