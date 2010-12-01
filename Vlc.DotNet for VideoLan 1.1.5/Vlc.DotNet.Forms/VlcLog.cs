using System.ComponentModel;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Forms
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public sealed class VlcLog
    {
        #region Verbosity enum

        public enum Verbosity
        {
            None = -1,
            Standard = 0,
            Warnings = 1,
            Debug = 2
        }

        #endregion

        internal VlcLog()
        {
            Verbose = Verbosity.None;
            LogInFile = false;
            LogInFilePath = "vlc_dotnet.log";
            ShowLoggerConsole = false;
        }

        [DefaultValue(Verbosity.None)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public Verbosity Verbose { get; set; }

        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public bool LogInFile { get; set; }

        [DefaultValue("vlc_dotnet.log")]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string LogInFilePath { get; set; }

        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public bool ShowLoggerConsole { get; set; }

        public override string ToString()
        {
            return "(VlcLog)";
        }
    }
}