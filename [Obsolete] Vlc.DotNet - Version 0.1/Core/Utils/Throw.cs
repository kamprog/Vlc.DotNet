using System;
using System.Diagnostics;
using Vlc.DotNet.Core.Utils.Extenders;

namespace Vlc.DotNet.Core.Utils
{
    public static class Throw
    {
        [DebuggerNonUserCode]
        public static void IfNullOrEmpty(string source)
        {
            IfNullOrEmpty(source, "String must not be null or empty.");
        }

        [DebuggerNonUserCode]
        public static void IfNullOrEmpty(string source, string exceptionMessage)
        {
            IfNullOrEmpty(source, new Exception(exceptionMessage));
        }

        [DebuggerNonUserCode]
        public static void IfNullOrEmpty(string source, Exception ex)
        {
            if (source.IsNullOrEmpty())
                throw ex;
        }

        [DebuggerNonUserCode]
        public static void IfNull(object source)
        {
            IfNull(source, "Object must not be null.");
        }

        [DebuggerNonUserCode]
        public static void IfNull(object source, string exceptionMessage)
        {
            IfNull(source, new Exception(exceptionMessage));
        }

        [DebuggerNonUserCode]
        public static void IfNull(object source, Exception ex)
        {
            if (source == null)
                throw ex;
        }

        [DebuggerNonUserCode]
        public static void Exception()
        {
            throw new Exception();
        }

        [DebuggerNonUserCode]
        public static void Exception(string message)
        {
            throw new Exception(message, null);
        }

        [DebuggerNonUserCode]
        public static void Exception(string message, Exception innerException)
        {
            throw new Exception(message, innerException);
        }
    }
}