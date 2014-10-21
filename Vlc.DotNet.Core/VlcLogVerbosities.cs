namespace Vlc.DotNet.Core
{
    /// <summary>
    /// Contains logging verbosities.
    /// </summary>
    public enum VlcLogVerbosities
    {
        /// <summary>
        /// No logging.
        /// </summary>
        None = -1,

        /// <summary>
        /// Standard logging verbosity.
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Logging verbosity containing warnings.
        /// </summary>
        Warnings = 1,

        /// <summary>
        /// Debug logging verbosity.
        /// </summary>
        Debug = 2
    }
}