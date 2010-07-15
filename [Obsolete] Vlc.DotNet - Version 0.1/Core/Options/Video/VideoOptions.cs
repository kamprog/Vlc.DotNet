using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Options
{
    public sealed class VideoOptions : OptionsBase
    {
        public VideoOptions()
        {
            myDicOptions.Add(VideoOptionEnum.Activate, new Option<bool>(true, value => value ? "video" : "no-video"));
            myDicOptions.Add(VideoOptionEnum.GrayScale, new Option<bool>(value => value ? "grayscale" : "no-grayscale"));
            myDicOptions.Add(VideoOptionEnum.FullScreen, new Option<bool>(value => value ? "fullscreen" : "no-fullscreen"));
            myDicOptions.Add(VideoOptionEnum.DropLateFrames, new Option<bool>(true, value => value ? "drop-late-frames" : "no-drop-late-frames"));
            myDicOptions.Add(VideoOptionEnum.SkipFrames, new Option<bool>(true, value => value ? "skip-frames" : "no-skip-frames"));
            myDicOptions.Add(VideoOptionEnum.QuietSynch, new Option<bool>(value => value ? "quiet-synchro" : "no-quiet-synchro"));
            myDicOptions.Add(VideoOptionEnum.Overlay, new Option<bool>(true, value => value ? "overlay" : "no-overlay"));
            myDicOptions.Add(VideoOptionEnum.DisableScreenSaver, new Option<bool>(true, value => value ? "disable-screensaver" : "no-disable-screensaver"));
            myDicOptions.Add(VideoOptionEnum.TitleShow, new Option<bool>(true, value => value ? "video-title-show" : "no-video-title-show"));
            myDicOptions.Add(VideoOptionEnum.TitleHideTimeOut, new Option<int>(5000, value => "video-title-timeout={0}".FormatIt(value), value => value >= 0));
            myDicOptions.Add(VideoOptionEnum.TitlePosition, new Option<VideoTitlePositions>(VideoTitlePositions.Bottom, value => "video-title-position={0}".FormatIt((int) value)));
            myDicOptions.Add(VideoOptionEnum.MouseHideTimeOut, new Option<int>(3000, value => "mouse-hide-timeout={0}".FormatIt(value), value => value >= 0));
        }

        public bool Activate
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.Activate]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.Activate].SetValue(value);
            }
        }

        public bool GrayScale
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.GrayScale]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.GrayScale].SetValue(value);
            }
        }

        public bool FullScreen
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.FullScreen]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.FullScreen].SetValue(value);
            }
        }

        public bool DropLateFrames
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.DropLateFrames]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.DropLateFrames].SetValue(value);
            }
        }

        public bool SkipFrames
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.SkipFrames]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.SkipFrames].SetValue(value);
            }
        }

        public bool QuietSynch
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.QuietSynch]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.QuietSynch].SetValue(value);
            }
        }

        public bool Overlay
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.Overlay]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.Overlay].SetValue(value);
            }
        }

        public bool DisableScreenSaver
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.DisableScreenSaver]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.DisableScreenSaver].SetValue(value);
            }
        }

        public bool TitleShow
        {
            get
            {
                return ((Option<bool>) myDicOptions[VideoOptionEnum.TitleShow]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.TitleShow].SetValue(value);
            }
        }

        public int TitleHideTimeOut
        {
            get
            {
                return ((Option<int>) myDicOptions[VideoOptionEnum.TitleHideTimeOut]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.TitleHideTimeOut].SetValue(value);
            }
        }

        public VideoTitlePositions TitlePosition
        {
            get
            {
                return ((Option<VideoTitlePositions>) myDicOptions[VideoOptionEnum.TitlePosition]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.TitlePosition].SetValue(value);
            }
        }

        public int MouseHideTimeOut
        {
            get
            {
                return ((Option<int>) myDicOptions[VideoOptionEnum.MouseHideTimeOut]).Value;
            }
            set
            {
                myDicOptions[VideoOptionEnum.MouseHideTimeOut].SetValue(value);
            }
        }

        #region Nested type: VideoOptionEnum

        internal enum VideoOptionEnum
        {
            Activate,
            GrayScale,
            FullScreen,
            DropLateFrames,
            SkipFrames,
            QuietSynch,
            Overlay,
            DisableScreenSaver,
            TitleShow,
            TitleHideTimeOut,
            TitlePosition,
            MouseHideTimeOut
        }

        #endregion
    }
}