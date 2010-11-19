using System.ComponentModel;
using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Medias
{
    public sealed class PauseMedia : MediaBase
    {
        private int myTime;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Time
        {
            get
            {
                return myTime;
            }
            set
            {
                myTime = value;
                Title = "Vlc : Pause : {0} sec.".FormatIt(value);
            }
        }

        public PauseMedia()
            : base("vlc://pause:{0}")
        {
            Title = "Vlc : Pause";
        }

        protected internal override string RetrieveMrl()
        {
            return Prefix.FormatIt(Time);
        }

        protected internal override string[] RetrieveOptions()
        {
            return new string[0];
        }
    }
}