using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc;

namespace Vlc.DotNet.Core
{
    /// <summary>
    /// VlcLogProperties class
    /// </summary>
    public sealed class VlcLogProperties : IEnumerable<VlcLogMessage>, IDisposable
    {
        private readonly IntPtr myLogInstance;

        internal VlcLogProperties()
        {
            myLogInstance = VlcContext.InteropManager.LoggingInterops.Open.Invoke(VlcContext.HandleManager.LibVlcHandle);
        }

        /// <summary>
        /// Get / Set log verbosity
        /// </summary>
        public VlcLogVerbosities Verbosity
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.LoggingInterops != null &&
                    VlcContext.InteropManager.LoggingInterops.GetVerbosity.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.LibVlcHandle != IntPtr.Zero)
                {
                    return (VlcLogVerbosities)VlcContext.InteropManager.LoggingInterops.GetVerbosity.Invoke(VlcContext.HandleManager.LibVlcHandle);
                }
                return VlcLogVerbosities.None;
            }
            set
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.LoggingInterops != null &&
                    VlcContext.InteropManager.LoggingInterops.GetVerbosity.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.LibVlcHandle != IntPtr.Zero)
                {
                    VlcContext.InteropManager.LoggingInterops.SetVerbosity.Invoke(VlcContext.HandleManager.LibVlcHandle, (uint)value);
                }
            }
        }
        /// <summary>
        /// Returns an enumerator that iterates VlcLogMessage.
        /// </summary>
        /// <returns>The current VlcLogMessage</returns>
        public IEnumerator<VlcLogMessage> GetEnumerator()
        {
            if (VlcContext.InteropManager != null &&
                VlcContext.InteropManager.LoggingInterops != null &&
                VlcContext.InteropManager.LoggingInterops.GetIterator.IsAvailable &&
                myLogInstance != IntPtr.Zero &&
                VlcContext.InteropManager.LoggingInterops.HasNext.IsAvailable &&
                VlcContext.InteropManager.LoggingInterops.FreeInstance.IsAvailable &&
                VlcContext.InteropManager.LoggingInterops.Clear.IsAvailable)
            {

                var iterator = VlcContext.InteropManager.LoggingInterops.GetIterator.Invoke(myLogInstance);

                while (VlcContext.InteropManager.LoggingInterops.HasNext.Invoke(iterator))
                {
                    var msg = new LogMessage();
                    VlcContext.InteropManager.LoggingInterops.Next.Invoke(iterator, ref msg);

                    var vlcLogMessage = new VlcLogMessage(
                        msg.i_severity,
                        Marshal.PtrToStringAnsi(msg.psz_type),
                        Marshal.PtrToStringAnsi(msg.psz_name),
                        Marshal.PtrToStringAnsi(msg.psz_header),
                        Marshal.PtrToStringAnsi(msg.psz_message));

                    yield return vlcLogMessage;
                }

                VlcContext.InteropManager.LoggingInterops.FreeInstance.Invoke(i);
                VlcContext.InteropManager.LoggingInterops.Clear.Invoke(myLogInstance);
            }
        }
        /// <summary>
        /// Returns an enumerator that iterates VlcLogMessage.
        /// </summary>
        /// <returns>The current VlcLogMessage</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            IntPtr i = VlcContext.InteropManager.LoggingInterops.GetIterator.Invoke(myLogInstance);

            while (VlcContext.InteropManager.LoggingInterops.HasNext.Invoke(i))
            {
                var msg = new LogMessage();
                VlcContext.InteropManager.LoggingInterops.Next.Invoke(i, ref msg);

                var vlcLogMessage = new VlcLogMessage(
                    msg.i_severity,
                    Marshal.PtrToStringAnsi(msg.psz_type),
                    Marshal.PtrToStringAnsi(msg.psz_name),
                    Marshal.PtrToStringAnsi(msg.psz_header),
                    Marshal.PtrToStringAnsi(msg.psz_message));

                yield return vlcLogMessage;
            }

            VlcContext.InteropManager.LoggingInterops.FreeInstance.Invoke(i);
            VlcContext.InteropManager.LoggingInterops.Clear.Invoke(myLogInstance);
        }

        public void Dispose()
        {
            if (myLogInstance != IntPtr.Zero &&
                VlcContext.InteropManager != null &&
                VlcContext.InteropManager.LoggingInterops != null &&
                VlcContext.InteropManager.LoggingInterops.Close.IsAvailable)
            {
                VlcContext.InteropManager.LoggingInterops.Close.Invoke(myLogInstance);
            }
        }
    }
}
