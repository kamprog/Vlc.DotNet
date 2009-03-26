using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Options
{
    public sealed class ScreenCaptureOptions : OptionsBase
    {
        internal ScreenCaptureOptions()
        {
            myDicOptions.Add(ScreenOptionEnum.Caching, new Option<int>(value => "screen-caching={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(ScreenOptionEnum.FPS, new Option<float>(value => "screen-fps={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(ScreenOptionEnum.Top, new Option<int>(value => "screen-top={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(ScreenOptionEnum.Left, new Option<int>(value => "screen-left={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(ScreenOptionEnum.Width, new Option<int>(value => "screen-width={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(ScreenOptionEnum.Height, new Option<int>(value => "screen-height={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(ScreenOptionEnum.FollowMouse, new Option<bool>(false, value => value ? "screen-follow-mouse" : "no-screen-follow-mouse"));
            myDicOptions.Add(ScreenOptionEnum.FragmentSize, new Option<int>(16, value => "screen-fragment-size={0}".FormatIt(value), value => value >= 0));
        }

        public int Caching
        {
            get
            {
                return ((Option<int>) myDicOptions[ScreenOptionEnum.Caching]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.Caching].SetValue(value);
            }
        }

        public float FPS
        {
            get
            {
                return ((Option<float>) myDicOptions[ScreenOptionEnum.FPS]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.FPS].SetValue(value);
            }
        }

        public int Top
        {
            get
            {
                return ((Option<int>) myDicOptions[ScreenOptionEnum.Top]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.Top].SetValue(value);
            }
        }

        public int Left
        {
            get
            {
                return ((Option<int>) myDicOptions[ScreenOptionEnum.Left]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.Left].SetValue(value);
            }
        }

        public int Width
        {
            get
            {
                return ((Option<int>) myDicOptions[ScreenOptionEnum.Width]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.Width].SetValue(value);
            }
        }

        public int Height
        {
            get
            {
                return ((Option<int>) myDicOptions[ScreenOptionEnum.Height]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.Height].SetValue(value);
            }
        }

        public bool FollowMouse
        {
            get
            {
                return ((Option<bool>) myDicOptions[ScreenOptionEnum.FollowMouse]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.FollowMouse].SetValue(value);
            }
        }

        public int FragmentSize
        {
            get
            {
                return ((Option<int>) myDicOptions[ScreenOptionEnum.FragmentSize]).Value;
            }
            set
            {
                myDicOptions[ScreenOptionEnum.FragmentSize].SetValue(value);
            }
        }

        #region Nested type: ScreenOptionEnum

        internal enum ScreenOptionEnum
        {
            Caching,
            FPS,
            Top,
            Left,
            Width,
            Height,
            FollowMouse,
            FragmentSize
        }

        #endregion
    }
}