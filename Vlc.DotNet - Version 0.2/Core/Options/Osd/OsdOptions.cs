using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Options
{
    public sealed class OsdOptions : OptionsBase
    {
        public OsdOptions()
        {
            myDicOptions.Add(OsdOptionEnum.Incrustations, new Option<bool>(true, value => value ? "spu" : "no-spu"));
            myDicOptions.Add(OsdOptionEnum.IsEnabled, new Option<bool>(true, value => value ? "osd" : "no-osd"));
            myDicOptions.Add(OsdOptionEnum.TextRenderer, new Option<string>("", value => "text-renderer=".FormatIt(value)));
        }

        public bool Incrustations
        {
            get
            {
                return ((Option<bool>)myDicOptions[OsdOptionEnum.Incrustations]).Value;
            }
            set
            {
                myDicOptions[OsdOptionEnum.Incrustations].SetValue(value);
            }

        }

        public bool IsEnabled
        {
            get
            {
                return ((Option<bool>)myDicOptions[OsdOptionEnum.IsEnabled]).Value;
            }
            set
            {
                myDicOptions[OsdOptionEnum.IsEnabled].SetValue(value);
            }
        }

        public string TextRenderer
        {
            get
            {
                return ((Option<string>)myDicOptions[OsdOptionEnum.TextRenderer]).Value;
            }
            set
            {
                myDicOptions[OsdOptionEnum.TextRenderer].SetValue(value);
            }
        }

        #region Nested type: VideoOptionEnum

        internal enum OsdOptionEnum
        {
            Incrustations,
            IsEnabled,
            TextRenderer
        }

        #endregion

    }
}
