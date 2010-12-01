using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interop;

namespace Vlc.DotNet.Forms
{
    //[TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public sealed class VlcManager : Component
    {
        private bool myIsDisposed;
        private IntPtr myVlcClient = IntPtr.Zero;

        public VlcManager()
        {
            PluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE;
            ScreenSaverEnabled = false;
            Logging = new VlcLog();
            IgnoreConfig = true;
        }

        internal IntPtr VlcClient
        {
            get
            {
                if (myVlcClient == IntPtr.Zero && !myIsDisposed)
                {
                    InitVlcClient();
                }
                return myVlcClient;
            }
        }

        [DefaultValue(CommonStrings.PLUGINS_PATH_DEFAULT_VALUE)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        [Description("Modules search path")]
        public string PluginsPath { get; set; }

        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        [Description("Enable screensaver")]
        public bool ScreenSaverEnabled { get; set; }

        [DefaultValue(false)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public VlcLog Logging { get; private set; }

        [DefaultValue(true)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        [Description("No configuration option will be loaded nor saved to config file")]
        public bool IgnoreConfig { get; set; }

        [DefaultValue(null)]
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public string UserAgent { get; set; }

        [Browsable(false)]
        public string Version
        {
            get { return LibVlcMethods.libvlc_get_version(); }
        }

        [Browsable(false)]
        public string ChangeSet
        {
            get { return LibVlcMethods.libvlc_get_changeset(); }
        }

        [Browsable(false)]
        public string Compiler
        {
            get { return LibVlcMethods.libvlc_get_compiler(); }
        }


        ~VlcManager()
        {
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (myIsDisposed)
                return;
            if (disposing)
            {
            }
            if (myVlcClient != IntPtr.Zero)
            {
                LibVlcMethods.libvlc_release(myVlcClient);
                myVlcClient = IntPtr.Zero;
            }
            myIsDisposed = true;
        }

        private void InitVlcClient()
        {
            if (DesignMode || myVlcClient != IntPtr.Zero)
                return;

            var args = new StringCollection();
            args.Add("-I");
            args.Add("dummy");
            if (IgnoreConfig)
                args.Add("--ignore-config");
            if (Logging != null && Logging.Verbose != VlcLog.Verbosity.None)
            {
                if (Logging.ShowLoggerConsole)
                    args.Add("--extraintf=logger");
                args.Add("--verbose=" + (int) Logging.Verbose);
                if (Logging.LogInFile)
                {
                    args.Add("--file-logging");
                    args.Add(@"--logfile=" + Logging.LogInFilePath);
                }
            }
            if (!string.IsNullOrEmpty(PluginsPath) && Directory.Exists(PluginsPath))
                args.Add("--plugin-path=" + PluginsPath);
            if (!ScreenSaverEnabled)
                args.Add(":disable-screensaver");
            if (!string.IsNullOrEmpty(UserAgent))
                args.Add("--user-agent=" + UserAgent);


            var argsArray = new string[args.Count];
            args.CopyTo(argsArray, 0);
            myVlcClient = LibVlcMethods.libvlc_new(args.Count, argsArray);
        }

        public override string ToString()
        {
            return "(VlcManager)";
        }
    }
}