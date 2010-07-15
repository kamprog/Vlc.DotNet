using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Options
{
    public sealed class SubTitle : OptionsBase
    {
        public SubTitle()
        {
            myDicOptions.Add(SubTitleOptionEnum.File, new Option<string>("", value => "sub-file={0}".FormatIt(value)));
            myDicOptions.Add(SubTitleOptionEnum.AutoDetectFile, new Option<bool>(true, value => value ? "sub-autodetect-file" : "no-sub-autodetect-file"));
            myDicOptions.Add(SubTitleOptionEnum.AutoDetectFuzzy, new Option<AutoDetectFuzzyState>(AutoDetectFuzzyState.None, value => "sub-autodetect-fuzzy={0}".FormatIt((int) value)));
            myDicOptions.Add(SubTitleOptionEnum.AutoDetectPath, new Option<string>("", value => "sub-autodetect-path={0}".FormatIt(value)));
            myDicOptions.Add(SubTitleOptionEnum.Margin, new Option<int>(0, value => "sub-margin={0}".FormatIt(value)));
            myDicOptions.Add(SubTitleOptionEnum.OverlaysFilter, new Option<string>("", value => "sub-filter={0}".FormatIt(value)));
        }

        public string File
        {
            get
            {
                return ((Option<string>)myDicOptions[SubTitleOptionEnum.File]).Value;
            }
            set
            {
                myDicOptions[SubTitleOptionEnum.File].SetValue(value);
            }
        }

        public bool AutoDetectFile
        {
            get
            {
                return ((Option<bool>)myDicOptions[SubTitleOptionEnum.AutoDetectFile]).Value;
            }
            set
            {
                myDicOptions[SubTitleOptionEnum.AutoDetectFile].SetValue(value);
            }
        }

        public AutoDetectFuzzyState AutoDetectFuzzy
        {
            get
            {
                return ((Option<AutoDetectFuzzyState>)myDicOptions[SubTitleOptionEnum.AutoDetectFuzzy]).Value;
            }
            set
            {
                myDicOptions[SubTitleOptionEnum.AutoDetectFuzzy].SetValue(value);
            }
        }

        public string AutoDetectPath
        {
            get
            {
                return ((Option<string>)myDicOptions[SubTitleOptionEnum.AutoDetectPath]).Value;
            }
            set
            {
                myDicOptions[SubTitleOptionEnum.AutoDetectPath].SetValue(value);
            }
        }

        public int Margin
        {
            get
            {
                return ((Option<int>)myDicOptions[SubTitleOptionEnum.Margin]).Value;
            }
            set
            {
                myDicOptions[SubTitleOptionEnum.Margin].SetValue(value);
            }
        }

        public string OverlaysFilter
        {
            get
            {
                return ((Option<string>)myDicOptions[SubTitleOptionEnum.OverlaysFilter]).Value;
            }
            set
            {
                myDicOptions[SubTitleOptionEnum.OverlaysFilter].SetValue(value);
            }
        }

        internal enum SubTitleOptionEnum
        {
            File,
            AutoDetectFile,
            AutoDetectFuzzy,
            AutoDetectPath,
            Margin,
            OverlaysFilter
        }
    }
}
