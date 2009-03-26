using System.IO;
using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Options
{
    public sealed class SnapShotOptions : OptionsBase
    {
        public SnapShotOptions()
        {
            myDicOptions.Add(SnapShotOptionEnum.Path, new Option<string>("", value => "snapshot-path={0}".FormatIt(value), Directory.Exists));
            myDicOptions.Add(SnapShotOptionEnum.Prefix, new Option<string>("", value => "snapshot-prefix={0}".FormatIt(value), value => !value.IsNullOrEmpty()));
            myDicOptions.Add(SnapShotOptionEnum.Format, new Option<SnapShotFormat>(SnapShotFormat.Png, value => "snapshot-format={0}".FormatIt(value.ToString())));
            myDicOptions.Add(SnapShotOptionEnum.Preview, new Option<bool>(true, value => value ? "snapshot-preview" : "no-snapshot-preview"));
            myDicOptions.Add(SnapShotOptionEnum.Sequential, new Option<bool>(true, value => value ? "snapshot-sequential" : "no-snapshot-sequential"));
            myDicOptions.Add(SnapShotOptionEnum.Width, new Option<int>(-1, value => "snapshot-width={0}".FormatIt(value), value => value >= -1));
            myDicOptions.Add(SnapShotOptionEnum.Height, new Option<int>(-1, value => "snapshot-height={0}".FormatIt(value), value => value >= -1));
        }

        public string Path
        {
            get
            {
                return ((Option<string>) myDicOptions[SnapShotOptionEnum.Path]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Path].SetValue(value);
            }
        }

        public string Prefix
        {
            get
            {
                return ((Option<string>) myDicOptions[SnapShotOptionEnum.Prefix]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Prefix].SetValue(value);
            }
        }

        public SnapShotFormat Format
        {
            get
            {
                return ((Option<SnapShotFormat>) myDicOptions[SnapShotOptionEnum.Format]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Format].SetValue(value);
            }
        }

        public bool Preview
        {
            get
            {
                return ((Option<bool>) myDicOptions[SnapShotOptionEnum.Preview]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Preview].SetValue(value);
            }
        }

        public bool Sequential
        {
            get
            {
                return ((Option<bool>) myDicOptions[SnapShotOptionEnum.Sequential]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Sequential].SetValue(value);
            }
        }

        public int Width
        {
            get
            {
                return ((Option<int>) myDicOptions[SnapShotOptionEnum.Width]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Width].SetValue(value);
            }
        }

        public int Height
        {
            get
            {
                return ((Option<int>) myDicOptions[SnapShotOptionEnum.Height]).Value;
            }
            set
            {
                myDicOptions[SnapShotOptionEnum.Height].SetValue(value);
            }
        }

        #region Nested type: SnapShotOptionEnum

        internal enum SnapShotOptionEnum
        {
            Path,
            Prefix,
            Format,
            Preview,
            Sequential,
            Width,
            Height
        }

        #endregion
    }
}