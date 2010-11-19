using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Options
{
    public sealed class AudioOptions : OptionsBase
    {
        public AudioOptions()
        {
            myDicOptions.Add(AudioOptionEnum.Activate, new Option<bool>(true, value => value ? "audio" : "no-audio"));
            myDicOptions.Add(AudioOptionEnum.Volume, new Option<int>(256, value => "volume={0}".FormatIt(value), value => value >= 0 && value <= 1024));
            myDicOptions.Add(AudioOptionEnum.VolumeStep, new Option<int>(256, value => "volume-step={0}".FormatIt(value), value => value >= 0 && value <= 1024));
            myDicOptions.Add(AudioOptionEnum.Rate, new Option<AudioRate>(AudioRate.Default, value => "aout-rate={0}".FormatIt((int) value)));
            myDicOptions.Add(AudioOptionEnum.HqResampling, new Option<bool>(true, value => value ? "hq-resampling" : "no-hq-resampling"));
            myDicOptions.Add(AudioOptionEnum.SPDIF, new Option<bool>(false, value => value ? "spdif" : "no-spdif"));
            myDicOptions.Add(AudioOptionEnum.ForceDolbySuround, new Option<ForceDolbySuroundState>(ForceDolbySuroundState.Auto, value => "force-dolby-surround={0}".FormatIt((int) value)));
            myDicOptions.Add(AudioOptionEnum.Desync, new Option<int>(0, value => "audio-desync={0}".FormatIt(value)));
            myDicOptions.Add(AudioOptionEnum.ReplayGainMode, new Option<ReplayGainModeType>(ReplayGainModeType.none, value => "audio-replay-gain-mode={0}".FormatIt(value.ToString())));
            myDicOptions.Add(AudioOptionEnum.ReplayGainPreAmp, new Option<decimal>(89M, value => "audio-replay-gain-preamp={0}".FormatIt(value)));
            myDicOptions.Add(AudioOptionEnum.ReplayGainDefault, new Option<decimal>(0M, value => "audio-replay-gain-default={0}".FormatIt(value)));
            myDicOptions.Add(AudioOptionEnum.ReplayGainPeakProtection, new Option<bool>(true, value => value ? "audio-replay-gain-peak-protection" : "no-audio-replay-gain-peak-protection"));
            myDicOptions.Add(AudioOptionEnum.ModuleOut, new Option<string>("", value => "aout={0}".FormatIt(value)));
            myDicOptions.Add(AudioOptionEnum.Filter, new Option<string>("", value => "audio-filter={0}".FormatIt(value)));
            myDicOptions.Add(AudioOptionEnum.Visual, new Option<string>("", value => "audio-visual={0}".FormatIt(value)));
        }

        public bool Activate
        {
            get
            {
                return ((Option<bool>) myDicOptions[AudioOptionEnum.Activate]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.Activate].SetValue(value);
            }
        }

        public int Volume
        {
            get
            {
                return ((Option<int>) myDicOptions[AudioOptionEnum.Volume]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.Volume].SetValue(value);
            }
        }

        public int VolumeStep
        {
            get
            {
                return ((Option<int>) myDicOptions[AudioOptionEnum.VolumeStep]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.VolumeStep].SetValue(value);
            }
        }

        public AudioRate Rate
        {
            get
            {
                return ((Option<AudioRate>) myDicOptions[AudioOptionEnum.Rate]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.Rate].SetValue(value);
            }
        }

        public bool HqResampling
        {
            get
            {
                return ((Option<bool>) myDicOptions[AudioOptionEnum.HqResampling]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.HqResampling].SetValue(value);
            }
        }

        public bool SPDIF
        {
            get
            {
                return ((Option<bool>) myDicOptions[AudioOptionEnum.SPDIF]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.SPDIF].SetValue(value);
            }
        }

        public ForceDolbySuroundState ForceDolbySuround
        {
            get
            {
                return ((Option<ForceDolbySuroundState>) myDicOptions[AudioOptionEnum.ForceDolbySuround]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.ForceDolbySuround].SetValue(value);
            }
        }

        public int Desync
        {
            get
            {
                return ((Option<int>) myDicOptions[AudioOptionEnum.Desync]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.Desync].SetValue(value);
            }
        }

        public ReplayGainModeType ReplayGainMode
        {
            get
            {
                return ((Option<ReplayGainModeType>) myDicOptions[AudioOptionEnum.ReplayGainMode]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.ReplayGainMode].SetValue(value);
            }
        }

        public decimal ReplayGainPreAmp
        {
            get
            {
                return ((Option<decimal>) myDicOptions[AudioOptionEnum.ReplayGainPreAmp]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.ReplayGainPreAmp].SetValue(value);
            }
        }

        public decimal ReplayGainDefault
        {
            get
            {
                return ((Option<decimal>) myDicOptions[AudioOptionEnum.ReplayGainDefault]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.ReplayGainDefault].SetValue(value);
            }
        }

        public bool ReplayGainPeakProtection
        {
            get
            {
                return ((Option<bool>) myDicOptions[AudioOptionEnum.ReplayGainPeakProtection]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.ReplayGainPeakProtection].SetValue(value);
            }
        }

        public string ModuleOut
        {
            get
            {
                return ((Option<string>) myDicOptions[AudioOptionEnum.ModuleOut]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.ModuleOut].SetValue(value);
            }
        }

        public string Filter
        {
            get
            {
                return ((Option<string>) myDicOptions[AudioOptionEnum.Filter]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.Filter].SetValue(value);
            }
        }

        public string Visual
        {
            get
            {
                return ((Option<string>) myDicOptions[AudioOptionEnum.Visual]).Value;
            }
            set
            {
                myDicOptions[AudioOptionEnum.Visual].SetValue(value);
            }
        }

        #region Nested type: AudioOptionEnum

        internal enum AudioOptionEnum
        {
            Activate,
            Volume,
            VolumeStep,
            Rate,
            HqResampling,
            SPDIF,
            ForceDolbySuround,
            Desync,
            ReplayGainMode,
            ReplayGainPreAmp,
            ReplayGainDefault,
            ReplayGainPeakProtection,
            ModuleOut,
            Filter,
            Visual
        }

        #endregion
    }
}