namespace Vlc.DotNet.Core
{
    /// <summary>
    /// Contains the default paths to the VLC binaries and plugins.
    /// </summary>
    public static class CommonStrings
    {
        /// <summary>
        /// The default binaries path for x64 systems - "C:\Program Files (x86)\VideoLAN\VLC\".
        /// </summary>
        public const string LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64 = @"C:\Program Files (x86)\VideoLAN\VLC\";

        /// <summary>
        /// The default binaries path for x86 systems - "C:\Program Files\VideoLAN\VLC\".
        /// </summary>
        public const string LIBVLC_DLLS_PATH_DEFAULT_VALUE_X86 = @"C:\Program Files\VideoLAN\VLC\";

        /// <summary>
        /// The default plugins path for x64 systems - "C:\Program Files (x86)\VideoLAN\VLC\plugins\".
        /// </summary>
        public const string PLUGINS_PATH_DEFAULT_VALUE_AMD64 = @"C:\Program Files (x86)\VideoLAN\VLC\plugins\";

        /// <summary>
        /// The default plugins path for x86 systems - "C:\Program Files\VideoLAN\VLC\plugins\".
        /// </summary>
        public const string PLUGINS_PATH_DEFAULT_VALUE_X86 = @"C:\Program Files\VideoLAN\VLC\plugins\";

        internal const string VLC_DOTNET_PROPERTIES_CATEGORY = "VideoLan DotNet";
    }
}