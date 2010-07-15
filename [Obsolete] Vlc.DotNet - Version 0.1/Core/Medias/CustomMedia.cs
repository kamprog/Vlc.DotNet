using System.ComponentModel;

namespace Vlc.DotNet.Core.Medias
{
    public class CustomMedia : MediaBase
    {
        private string myMrl;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Mrl
        {
            get
            {
                return myMrl;
            }
            set
            {
                myMrl = value;
            }
        }

        public CustomMedia()
            : base("")
        {
        }

        public CustomMedia(string mrl)
            : this()
        {
            Mrl = mrl;
        }

        protected internal override string RetrieveMrl()
        {
            return Mrl;
        }

        protected internal override string[] RetrieveOptions()
        {
            return new string[0];
        }
    }
}
