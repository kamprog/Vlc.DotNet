using System;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interop
{
    internal static partial class LibVlcMethods
    {
        #region LibVLC error handling

        [DllImport("libvlc")]
        public static extern string libvlc_errmsg();

        [DllImport("libvlc")]
        public static extern void libvlc_clearerr();

        // libvlc_vprinterr

        #endregion

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_new(int argc, string[] argv);

        // libvlc_new_with_builtins

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_release(IntPtr instance);

        // libvlc_retain
        // libvlc_add_intf
        // libvlc_set_exit_handler
        // libvlc_wait
        // libvlc_set_user_agent

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern string libvlc_get_version();

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern string libvlc_get_compiler();

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern string libvlc_get_changeset();

        #region LibVLC asynchronous events

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EventCallbackDelegate(ref libvlc_event_t eventType, IntPtr userData);

        #endregion

        [DllImport("libvlc")]
        public static extern int libvlc_event_attach(IntPtr eventManagerInstance, libvlc_event_e eventType, IntPtr callback, IntPtr userData);

        [DllImport("libvlc")]
        public static extern void libvlc_event_detach(IntPtr eventManagerInstance, libvlc_event_e eventType, IntPtr callback, IntPtr userData);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr libvlc_event_type_name(libvlc_event_t eventType);

        #endregion

        #region LibVLC logging

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint libvlc_get_log_verbosity(IntPtr instance);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_set_log_verbosity(IntPtr logger, uint level);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_log_open(IntPtr instance);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_log_close(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint libvlc_log_count(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_log_clear(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_log_get_iterator(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_log_iterator_free(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_log_iterator_has_next(IntPtr iterator);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern libvlc_log_message_t libvlc_log_iterator_next(IntPtr iterator, ref libvlc_log_message_t buffer);

        #endregion

        // libvlc_free
    }
}