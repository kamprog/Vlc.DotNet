namespace Vlc.DotNet.Core.Interops.Signatures
{
    namespace LibVlc
    {
        public struct LogMessage
        {
            public int severity;
            public string type;
            public string name;
            public string header;
            public string message;
        }
	}
}
