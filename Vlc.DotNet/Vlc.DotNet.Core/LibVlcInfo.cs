using System;
using System.Collections.Generic;
using System.Text;

namespace Vlc.DotNet.Core
{
    public static class LibVlcInfo
    {
        public static string Changeset
        {
            get
            {
                return Wrapper.LibVlc.Changeset;
            }
        }
        public static string Compiler
        {
            get
            {
                return Wrapper.LibVlc.Compiler;
            }
        }
        public static string Version
        {
            get
            {
                return Wrapper.LibVlc.Version;
            }
        }
    }
}
