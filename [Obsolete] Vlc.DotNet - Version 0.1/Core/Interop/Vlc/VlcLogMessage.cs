namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcLogMessage
    {
        internal libvlc_log_message msg;

        internal VlcLogMessage()
        {
            msg = new libvlc_log_message();
        }

        public uint Size
        {
            get
            {
                return msg.sizeof_msg;
            }
        }

        public int Severity
        {
            get
            {
                return msg.i_severity;
            }
        }

        public string Header
        {
            get
            {
                return msg.psz_header;
            }
        }

        public string Name
        {
            get
            {
                return msg.psz_name;
            }
        }

        public string Type
        {
            get
            {
                return msg.psz_type;
            }
        }

        public string Message
        {
            get
            {
                return msg.psz_message;
            }
        }
    }
}