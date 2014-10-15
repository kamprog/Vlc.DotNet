using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vlc.DotNet.Core.Interops
{
    internal class Win32Exception : ExternalException
    {
        private readonly int myNativeErrorCode;

        public Win32Exception()
            : this(Marshal.GetLastWin32Error())
        {
        }

        public Win32Exception(int error)
            : this(error, GetErrorMessage(error))
        {
        }

        public Win32Exception(string message)
            : this(Marshal.GetLastWin32Error(), message)
        {
        }

        public Win32Exception(int error, string message)
            : base(message)
        {
            myNativeErrorCode = error;
        }


        public Win32Exception(string message, Exception innerException)
            : base(message, innerException)
        {
            myNativeErrorCode = Marshal.GetLastWin32Error();
        }


        public int NativeErrorCode
        {
            get { return myNativeErrorCode; }
        }

        private static string GetErrorMessage(int error)
        {
            var lpBuffer = new StringBuilder(0x100);
            if (FormatMessage(0x3200, new HandleRef(null, IntPtr.Zero), error, 0, lpBuffer, lpBuffer.Capacity + 1, IntPtr.Zero) == 0)
            {
                return ("Unknown error (0x" + Convert.ToString(error, 0x10) + ")");
            }
            int length = lpBuffer.Length;
            while (length > 0)
            {
                char ch = lpBuffer[length - 1];
                if ((ch > ' ') && (ch != '.'))
                {
                    break;
                }
                length--;
            }
            return lpBuffer.ToString(0, length);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int FormatMessage(int dwFlags, SafeHandle lpSource, uint dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr[] arguments);

        #region Nested type: HandleRef

        [StructLayout(LayoutKind.Sequential), ComVisible(true)]
        public struct HandleRef
        {
            internal object m_wrapper;
            internal IntPtr m_handle;

            public HandleRef(object wrapper, IntPtr handle)
            {
                m_wrapper = wrapper;
                m_handle = handle;
            }

            public object Wrapper
            {
                get { return m_wrapper; }
            }

            public IntPtr Handle
            {
                get { return m_handle; }
            }

            public static explicit operator IntPtr(HandleRef value)
            {
                return value.m_handle;
            }

            public static IntPtr ToIntPtr(HandleRef value)
            {
                return value.m_handle;
            }
        }

        #endregion
    }
}