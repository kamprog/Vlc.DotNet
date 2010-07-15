using System;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interop.Event;

namespace Vlc.DotNet.Core.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct libvlc_exception_t
    {
        public Int32 b_raised;
        public Int32 i_code;
        public IntPtr psz_message;

        public void Initalize()
        {
            InteropMethods.libvlc_exception_init(out this);
        }

        public void CheckException()
        {
            bool wasRaised = InteropMethods.libvlc_exception_raised(ref this) == 1;
            string message = (wasRaised) ? InteropMethods.libvlc_exception_get_message(ref this) : string.Empty;
            InteropMethods.libvlc_exception_clear(ref this);
            if (wasRaised) throw new Exception(message);    //TODO: create VlcException class
        }
        public bool HadException()
        {
            bool wasRaised = InteropMethods.libvlc_exception_raised(ref this) == 1;
            InteropMethods.libvlc_exception_clear(ref this);
            return wasRaised;
        }
    }

    #region internal struct libvlc_log_message
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct libvlc_log_message
    {
        public uint sizeof_msg;
        public int i_severity;
        public string psz_type;
        public string psz_name;
        public string psz_header;
        public string psz_message;
    }
    #endregion

    #region internal struct libvlc_track_description_t
    /// <summary>
    /// Description for video, audio tracks and subtitles. It contains
    /// id, name (description string) and pointer to next record.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct libvlc_track_description_t
    {

        /// int
        public int i_id;

        /// char*
        //[MarshalAs(UnmanagedType.LPStr)]
        public IntPtr psz_name;

        /// libvlc_track_description_t*
        public IntPtr p_next;
    }
    #endregion

    #region public struct libvlc_audio_output_t
    /// <summary>
    /// Description for audio output. It contains
    /// name, description and pointer to next record.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct libvlc_audio_output_t
    {
        /// char*
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_name;

        /// char*
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_description;

        /// libvlc_audio_output_t*
        public IntPtr p_next;
    }
    #endregion

    #region internal struct libvlc_rectangle_t
    /// <summary>
    /// Rectangle type for video geometry
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct libvlc_rectangle_t
    {

        /// int
        public int top;

        /// int
        public int left;

        /// int
        public int bottom;

        /// int
        public int right;
    }
    #endregion

    #region Vlc Enums

    public enum VlcState
    {
        NothingSpecial = 0,
        Opening,
        Buffering,
        Playing,
        Paused,
        Stopped,
        Forward,
        Backward,
        Ended,
        Error
    }

    public enum libvlc_meta_t
    {
        Title,
        Artist,
        Genre,
        Copyright,
        Album,
        TrackNumber,
        Description,
        Rating,
        Date,
        Setting,
        URL,
        Language,
        NowPlaying,
        Publisher,
        EncodedBy,
        ArtworkURL,
        TrackID
    }

    #region public enum AudioOutputDeviceTypes
    /// <summary>
    /// Audio device types
    /// </summary>
    public enum AudioOutputDeviceTypes
    {
        Error = -1,
        Mono = 1,
        Stereo = 2,
        _2F2R = 4,
        _3F2R = 5,
        _5_1 = 6,
        _6_1 = 7,
        _7_1 = 8,
        SPDIF = 10,
    }
    #endregion

    #region public enum AudioOutputChannel
    /// <summary>
    /// Audio channels
    /// </summary>
    public enum AudioOutputChannel
    {
        Error = -1,
        Stereo = 1,
        RStereo = 2,
        Left = 3,
        Right = 4,
        Dolbys = 5,
    }
    #endregion

    #endregion

    #region Vlc Interop Methods

    /// <summary>
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    internal static class InteropMethods
    {
        #region Exception Methods

        #region internal static extern void libvlc_exception_init(out libvlc_exception_t p_exception)
        /// <summary>
        /// Initialize an exception structure. This can be called several times to reuse 
        /// an exception structure.
        /// </summary>
        /// <param name="p_exception">the exception to initialize</param>
        [DllImport("libvlc")]
        internal static extern void libvlc_exception_init(out libvlc_exception_t p_exception);
        #endregion

        #region internal static extern int libvlc_exception_raised(ref libvlc_exception_t p_exception)
        /// <summary>
        /// Has an exception been raised ?
        /// </summary>
        /// <param name="p_exception">the exception to query</param>
        /// <returns>0 if no exception raised, 1 else</returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_exception_raised(ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_exception_clear(ref libvlc_exception_t p_exception)
        /// <summary>
        /// Clear an exception object so it can be reused.  The exception object must be initialized
        /// </summary>
        /// <param name="p_exception">the exception to clear</param>
        [DllImport("libvlc")]
        internal static extern void libvlc_exception_clear(ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern String libvlc_exception_get_message(ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get exception message
        /// </summary>
        /// <param name="p_exception">the exception to query</param>
        /// <returns>the exception message or NULL if not applicable (exception not raised for example)</returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_exception_get_message(ref libvlc_exception_t p_exception);
        #endregion

        #endregion

        #region Core Methods

        #region internal static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] argv, ref libvlc_exception_t exception)
        /// <summary>
        /// Create an initialized libvlc instance
        /// </summary>
        /// <param name="argc">the number of arguments</param>
        /// <param name="argv">command-line-type arguments as string array</param>
        /// <param name="exception">an initialized exception pointer</param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] argv, ref libvlc_exception_t exception);
        #endregion

        #region internal static extern int libvlc_get_vlc_id(IntPtr p_instance)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_get_vlc_id(IntPtr p_instance);
        #endregion

        #region internal static extern void libvlc_release (IntPtr p_instance)
        /// <summary>
        /// Decrement the reference count of a libvlc instance, and destroy it if it reaches zero.
        /// </summary>
        /// <param name="p_instance"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_release(IntPtr p_instance);
        #endregion

        #region internal static extern void libvlc_retain (IntPtr p_instance)
        /// <summary>
        /// Increments the reference count of a libvlc instance.
        /// </summary>
        /// <param name="p_instance"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_retain(IntPtr p_instance);
        #endregion

        #region internal static extern void libvlc_add_intf(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Try to start a user interface for the libvlc instance, and wait until the user exits.
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="name"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_add_intf(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string name, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_wait (IntPtr p_instance)
        /// <summary>
        /// Waits until an interface causes the instance to exit.
        /// </summary>
        /// <param name="p_instance"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_wait(IntPtr p_instance);
        #endregion

        #region internal static extern String libvlc_get_version ()
        /// <summary>
        /// Retrieve libvlc version.
        /// </summary>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_get_version();
        #endregion

        #region internal static extern String libvlc_get_compiler ()
        /// <summary>
        /// Retrieve libvlc compiler version.
        /// </summary>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_get_compiler();
        #endregion

        #region internal static extern String libvlc_get_changeset ()
        /// <summary>
        /// Retrieve libvlc changeset. 
        /// </summary>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_get_changeset();
        #endregion

        #endregion

        #region Media Methods

        #region internal static extern IntPtr libvlc_media_new(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string psz_mrl, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Create a media with the given MRL.
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_mrl"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_new(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string psz_mrl, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern IntPtr libvlc_media_new_as_node(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string psz_name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Create a media as an empty node with the passed name.
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_new_as_node(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string psz_name, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_media_add_option(IntPtr p_media, [MarshalAs(UnmanagedType.LPStr)] string ppsz_options, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Add an option to the media.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="ppsz_options"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_add_option(IntPtr p_media, [MarshalAs(UnmanagedType.LPStr)] string ppsz_options, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_media_retain(IntPtr p_media)
        /// <summary>
        /// Retain a reference to a media descriptor object (libvlc_media_t).
        /// </summary>
        /// <param name="p_media"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_retain(IntPtr p_media);
        #endregion

        #region internal static extern void libvlc_media_release(IntPtr p_media)
        /// <summary>
        /// Decrement the reference count of a media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_release(IntPtr p_media);
        #endregion

        #region internal static extern String libvlc_media_get_mrl(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the media resource locator (mrl) from a media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_media_get_mrl(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern IntPtr libvlc_media_duplicate(IntPtr p_media)
        /// <summary>
        /// Duplicate a media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_duplicate(IntPtr p_media);
        #endregion

        #region internal static extern String libvlc_media_get_meta(IntPtr p_media, libvlc_meta_t e_meta, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Read the meta of the media.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="e_meta"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_media_get_meta(IntPtr p_media, libvlc_meta_t e_meta, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern VlcState libvlc_media_get_state(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get current state of media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern VlcState libvlc_media_get_state(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern IntPtr libvlc_media_subitems(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get subitems of media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_subitems(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern IntPtr libvlc_media_event_manager(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get event manager from media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_event_manager(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern long libvlc_media_get_duration(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get duration of media descriptor object item.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern long libvlc_media_get_duration(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern bool libvlc_media_is_preparsed(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get preparsed status for media descriptor object.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_is_preparsed(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion


        //    //Sets media descriptor's user_data.
        //[DllImport("libvlc")]
        //internal static extern void libvlc_media_set_user_data(IntPtr p_media, void* p_new_user_data, ref libvlc_exception_t p_exception);

        //    //Get media descriptor's user_data. 
        //void * 	libvlc_media_get_user_data (IntPtr p_media, ref libvlc_exception_t p_exception);

        #endregion

        #region Media Player Methods
        #region internal static extern IntPtr libvlc_media_player_new(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_new(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_player_new_from_media(IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_new_from_media(IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_release(IntPtr p_media)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_release(IntPtr p_media);
        #endregion
        #region internal static extern void libvlc_media_player_retain(IntPtr p_media)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_retain(IntPtr p_media);
        #endregion
        #region internal static extern void libvlc_media_player_set_media(IntPtr p_media_player, IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_media(IntPtr p_media_player, IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_player_get_media(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_get_media(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_player_event_manager(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_event_manager(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern bool libvlc_media_player_is_playing(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_is_playing(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_play(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_play(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_pause(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_pause(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_stop(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_stop(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_media_player_set_nsobject(IntPtr p_media_player, IntPtr handle, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Set the agl handler where the media player should render its video output.
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="handle"></param>
        /// <param name="p_exception"></param>
        //[DllImport("libvlc")]
        //internal static extern void libvlc_media_player_set_nsobject(IntPtr p_media_player, IntPtr handle, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_player_get_nsobject(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the agl handler previously set with libvlc_media_player_set_agl().
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        //[DllImport("libvlc")]
        //internal static extern IntPtr libvlc_media_player_get_nsobject(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_media_player_set_agl(IntPtr p_media_player, UInt32 handle, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Set the agl handler where the media player should render its video output.
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="handle"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_agl(IntPtr p_media_player, UInt32 handle, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern UInt32 libvlc_media_player_get_agl(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the agl handler previously set with libvlc_media_player_set_agl().
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern UInt32 libvlc_media_player_get_agl(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_set_xwindow(IntPtr p_media_player, UInt32 handle, ref libvlc_exception_t p_exception)
        /// <summary>
        ///  Set an X Window System drawable where the media player should render its
        ///  video output. If LibVLC was built without X11 output support, then this has
        ///  no effects.
        ///  The specified identifier must correspond to an existing Input/Output class
        ///  X11 window. Pixmaps are <b>not</b> supported. The caller shall ensure that
        ///  the X11 server is the same as the one the VLC instance has been configured
        ///  with.
        ///  If XVideo is <b>not</b> used, it is assumed that the drawable has the
        ///  following properties in common with the default X11 screen: depth, scan line
        ///  pad, black pixel. This is a bug.
        ///  </summary>
        /// <param name="p_media_player"></param>
        /// <param name="handle">drawable the ID of the X window</param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_xwindow(IntPtr p_media_player, UInt32 handle, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern UInt32 libvlc_media_player_get_xwindow(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the X Window System window identifier previously set with
        /// libvlc_media_player_set_xwindow(). Note that this will return the identifier
        /// even if VLC is not currently using it (for instance if it is playing an
        /// audio-only input).
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns>an X window ID, or 0 if none where set.</returns>
        [DllImport("libvlc")]
        internal static extern UInt32 libvlc_media_player_get_xwindow(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_set_hwnd(IntPtr p_media_player, IntPtr handle, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Set a Win32/Win64 API window handle (HWND) where the media player should
        /// render its video output. If LibVLC was built without Win32/Win64 API output
        /// support, then this has no effects.
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="handle"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_hwnd(IntPtr p_media_player, IntPtr handle, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_player_get_hwnd(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the Windows API window handle (HWND) previously set with
        /// libvlc_media_player_set_hwnd(). The handle will be returned even if LibVLC
        /// is not currently outputting any video to it.
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_get_hwnd(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern long libvlc_media_player_get_length(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern long libvlc_media_player_get_length(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern long libvlc_media_player_get_time(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern long libvlc_media_player_get_time(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_set_time(IntPtr p_media_player, long time, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="time"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_time(IntPtr p_media_player, long time, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern float libvlc_media_player_get_position(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern float libvlc_media_player_get_position(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_set_position(IntPtr p_media_player, float position, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="position"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_position(IntPtr p_media_player, float position, ref libvlc_exception_t p_exception);
        #endregion
        
        #region internal static extern void libvlc_media_player_set_chapter(IntPtr p_media_player, int chapter, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="chapter"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_chapter(IntPtr p_media_player, int chapter, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_player_get_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_player_get_chapter_count(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_chapter_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        
        #region internal static extern bool libvlc_media_player_will_play(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_will_play(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern int libvlc_media_player_get_chapter_count_for_title(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get title chapter count
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_chapter_count_for_title(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_media_player_set_title(IntPtr p_media_player, int title, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="title"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_title(IntPtr p_media_player, int title, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_player_get_title(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_title(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_player_get_title_count(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_title_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_media_player_previous_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_previous_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_next_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_next_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern float libvlc_media_player_get_rate(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern float libvlc_media_player_get_rate(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_player_set_rate(IntPtr p_media_player, float rate, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="rate"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_rate(IntPtr p_media_player, float rate, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern VlcState libvlc_media_player_get_state(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern VlcState libvlc_media_player_get_state(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern float libvlc_media_player_get_fps(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern float libvlc_media_player_get_fps(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_media_player_has_vout(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_has_vout(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_media_player_is_seekable(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_is_seekable(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_media_player_can_pause(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_can_pause(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_track_description_release(ref libvlc_track_description_t p_track_description)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_track_description"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_track_description_release(IntPtr p_track_description);
        #endregion
        #endregion

        #region Video Methods
        #region internal static extern void libvlc_toggle_fullscreen(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_toggle_fullscreen(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_set_fullscreen(IntPtr p_media_player, bool b_fullscreen, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="b_fullscreen"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_set_fullscreen(IntPtr p_media_player, bool b_fullscreen, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_get_fullscreen(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_get_fullscreen(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        
        #region internal static extern int libvlc_video_get_height(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_height(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_video_get_width(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_width(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        
        #region internal static extern String libvlc_video_get_aspect_ratio(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_video_get_aspect_ratio(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_aspect_ratio(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="psz_geometry"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_aspect_ratio(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern float libvlc_video_get_scale(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern float libvlc_video_get_scale(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_scale(IntPtr p_media_player, float i_factor, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="i_factor"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_scale(IntPtr p_media_player, float i_factor, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern int libvlc_video_get_spu(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_spu(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_video_get_spu_count(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_spu_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_video_get_spu_description(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_video_get_spu_description(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_spu(IntPtr p_media_player, int i_spu, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="i_spu"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_spu(IntPtr p_media_player, int i_spu, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_video_set_subtitle_file(IntPtr p_media_player, String psz_subtitle, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="psz_subtitle"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_set_subtitle_file(IntPtr p_media_player, String psz_subtitle, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern IntPtr libvlc_video_get_title_description(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_video_get_title_description(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern IntPtr libvlc_video_get_chapter_description(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_video_get_chapter_description(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern String libvlc_video_get_crop_geometry(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_video_get_crop_geometry(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_crop_geometry(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="psz_geometry"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_crop_geometry(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry, ref libvlc_exception_t p_exception);
        #endregion
        
        #region internal static extern void libvlc_toggle_teletext(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_toggle_teletext(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_video_get_teletext(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_teletext(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_teletext(IntPtr p_media_player, int i_page, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="i_page"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_teletext(IntPtr p_media_player, int i_page, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern int libvlc_video_get_track_count(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_track_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_video_get_track_description(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the description of available video tracks.
        /// </summary>
        /// <param name="p_media_player">media player</param>
        /// <param name="p_exception">an initialized exception</param>
        /// <returns>list with description of available video tracks</returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_video_get_track_description(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_video_get_track(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_track(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_track(IntPtr p_media_player, int i_track, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="i_track"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_track(IntPtr p_media_player, int i_track, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_video_take_snapshot(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_filepath, uint i_width, uint i_height, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Take a snapshot of the current video window.
        /// 
        /// If i_width AND i_height is 0, original size is used.
        /// If i_width XOR i_height is 0, original aspect-ratio is preserved.
        /// </summary>
        /// <param name="p_media_player">media player instance</param>
        /// <param name="psz_filepath">the path where to save the screenshot to</param>
        /// <param name="i_width">the snapshot's width</param>
        /// <param name="i_height">the snapshot's height</param>
        /// <param name="p_exception">an initialized exception pointer</param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_take_snapshot(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_filepath, uint i_width, uint i_height, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern libvlc_audio_output_t libvlc_audio_output_list_get(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get the list of available audio outputs
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns>list of available audio outputs, at the end free it with libvlc_audio_output_list_release</returns>
        [DllImport("libvlc")]
        internal static extern libvlc_audio_output_t libvlc_audio_output_list_get(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_track_description_release(libvlc_track_description p_track_description)
        /// <summary>
        /// Free the list of available audio outputs
        /// </summary>
        /// <param name="p_list"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_output_list_release(libvlc_audio_output_t p_list);
        #endregion

        #region internal static extern void libvlc_video_resize(IntPtr p_media_player, int width, int height, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_resize(IntPtr p_media_player, int width, int height, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_video_reparent(IntPtr p_media_player, IntPtr parent, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="parent"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_video_reparent(IntPtr p_media_player, IntPtr parent, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_video_set_size(IntPtr p_instance, int width, int height, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_size(IntPtr p_instance, int width, int height, ref libvlc_exception_t p_exception);
        #endregion
        #endregion

        #region Audio Methods

        #region internal static extern int libvlc_audio_output_set(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_name)
        /// <summary>
        /// Set the audio output.
        /// Change will be applied after stop and play.
        /// </summary>
        /// <param name="p_instance">libvlc instance</param>
        /// <param name="psz_name">name of audio output, use psz_name of libvlc_audio_output_t</param>
        /// <returns>true if function succeded</returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_audio_output_set(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_name);
        #endregion
        #region internal static extern int libvlc_audio_output_device_count(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output)
        /// <summary>
        /// Get count of devices for audio output, these devices are hardware oriented
        /// like analog or digital output of sound card
        /// </summary>
        /// <param name="p_instance">libvlc instance</param>
        /// <param name="psz_audio_output">name of audio output, see libvlc_audio_output_t</param>
        /// <returns>number of devices</returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_audio_output_device_count(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output);
        #endregion
        #region internal static extern String libvlc_audio_output_device_longname(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output, int i_device)
        /// <summary>
        /// Get long name of device, if not available short name given
        /// </summary>
        /// <param name="p_instance">libvlc instance</param>
        /// <param name="psz_audio_output">name of audio output, \see libvlc_audio_output_t</param>
        /// <param name="i_device">device index</param>
        /// <returns>long name of device</returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_audio_output_device_longname(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output, int i_device);
        #endregion
        #region internal static extern String libvlc_audio_output_device_id(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output, int i_device)
        /// <summary>
        /// Get id name of device
        /// </summary>
        /// <param name="p_instance">libvlc instance</param>
        /// <param name="psz_audio_output">name of audio output, \see libvlc_audio_output_t</param>
        /// <param name="i_device">device index</param>
        /// <returns>id name of device, use for setting device, need to be free after use</returns>
        [DllImport("libvlc")]
        internal static extern String libvlc_audio_output_device_id(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output, int i_device);
        #endregion
        #region internal static extern void libvlc_audio_output_device_set(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_device_id)
        /// <summary>
        /// Set device for using
        /// </summary>
        /// <param name="p_instance">libvlc instance</param>
        /// <param name="psz_audio_output">name of audio output, \see libvlc_audio_output_t</param>
        /// <param name="psz_device_id">device</param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_output_device_set(IntPtr p_instance, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_audio_output, [In()] [MarshalAs(UnmanagedType.LPStr)] string psz_device_id);
        #endregion
        #region internal static extern AudioOutputDeviceTypes libvlc_audio_output_device_set(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get current audio device type. Device type describes something like
        /// character of output sound - stereo sound, 2.1, 5.1 etc
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns>the audio devices type \see libvlc_audio_output_device_types_t</returns>
        [DllImport("libvlc")]
        internal static extern AudioOutputDeviceTypes libvlc_audio_output_device_set(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_audio_output_set_device_type(IntPtr p_instance, AudioOutputDeviceTypes device_type, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Set current audio device type.
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="device_type">the audio device type, according to \see libvlc_audio_output_device_types_t</param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_output_set_device_type(IntPtr p_instance, AudioOutputDeviceTypes device_type, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern void libvlc_audio_toggle_mute(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_toggle_mute(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_audio_get_mute(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_audio_get_mute(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_audio_set_mute(IntPtr p_instance, bool status, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="status"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_mute(IntPtr p_instance, bool status, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern int libvlc_audio_get_volume(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_volume(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_audio_set_volume(IntPtr p_instance, int i_volume, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="i_volume"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_volume(IntPtr p_instance, int i_volume, ref libvlc_exception_t p_exception);
        #endregion
        
        #region internal static extern int libvlc_audio_get_track_count(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_track_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern libvlc_track_description_t libvlc_audio_get_track_description(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// Get current audio track.
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_audio_get_track_description(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_audio_get_track(IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_track(IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_audio_set_track(IntPtr p_media_player, int i_track, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_media_player"></param>
        /// <param name="i_track"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_track(IntPtr p_media_player, int i_track, ref libvlc_exception_t p_exception);
        #endregion

        #region internal static extern AudioOutputChannel libvlc_audio_get_channel(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern AudioOutputChannel libvlc_audio_get_channel(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_audio_set_channel(IntPtr p_instance, AudioOutputChannel i_channel, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="i_channel"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_channel(IntPtr p_instance, AudioOutputChannel i_channel, ref libvlc_exception_t p_exception);
        #endregion
        #endregion

        #region Event Manager Methods
        [DllImport("libvlc")]
        internal static extern void libvlc_event_attach(IntPtr p_event_manager, VlcEventType i_event_type, VlcCallback f_callback, IntPtr p_user_data, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern void libvlc_event_detach(IntPtr p_event_manager, VlcEventType i_event_type, VlcCallback f_callback, IntPtr p_user_data, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern string libvlc_event_type_name(VlcEventType event_type);
        #endregion

        #region Logging Methods
        [DllImport("libvlc")]
        internal static extern uint libvlc_get_log_verbosity(IntPtr p_instance, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern void libvlc_set_log_verbosity(IntPtr p_instance, uint level, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_log_open(IntPtr p_instance, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern void libvlc_log_close(IntPtr p_log, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern uint libvlc_log_count(IntPtr p_log, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern void libvlc_log_clear(IntPtr p_log, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_log_get_iterator(IntPtr p_log, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern void libvlc_log_iterator_free(IntPtr p_iter, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern bool libvlc_log_iterator_has_next(IntPtr p_iter, ref libvlc_exception_t p_exception);
        [DllImport("libvlc")]
        internal static extern libvlc_log_message libvlc_log_iterator_next(IntPtr p_iter, ref libvlc_log_message p_buffer, ref libvlc_exception_t p_exception);

        #endregion

        #region MediaList Methods

        #region internal static extern IntPtr libvlc_media_list_new(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_new(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_release(IntPtr p_mlist)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_release(IntPtr p_mlist);
        #endregion
        #region internal static extern void libvlc_media_list_retain(IntPtr p_mlist)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_retain(IntPtr p_mlist);
        #endregion
        #region internal static extern void libvlc_media_list_add_file_content(IntPtr p_mlist, string psz_uri, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="psz_uri"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_add_file_content(IntPtr p_mlist, string psz_uri, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_set_media(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_set_media(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_list_media(IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_media(IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_add_media(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_add_media(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_insert_media(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_insert_media(IntPtr p_mlist, IntPtr p_media, int i_index, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_remove_index(IntPtr p_mlist, int i_index, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="i_index"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_remove_index(IntPtr p_mlist, int i_index, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_list_count(IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_list_count(IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_list_item_at_index(IntPtr p_mlist, int i_index, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="i_index"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_item_at_index(IntPtr p_mlist, int i_index, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_list_index_of_item(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_list_index_of_item(IntPtr p_mlist, IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_media_list_is_readonly(IntPtr p_mlist)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_media_list_is_readonly(IntPtr p_mlist);
        #endregion
        #region internal static extern void libvlc_media_list_lock(IntPtr p_mlist)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_lock(IntPtr p_mlist);
        #endregion
        #region internal static extern void libvlc_media_list_unlock(IntPtr p_mlist)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_unlock(IntPtr p_mlist);
        #endregion
        #region internal static extern IntPtr libvlc_media_list_flat_view(IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_flat_view(IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_list_hierarchical_view(IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_hierarchical_view(IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_list_hierarchical_node_view(IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_hierarchical_node_view(IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern IntPtr libvlc_media_list_event_manager(IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_event_manager(IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion

        #endregion

        #region Media List Player Methods

        #region internal static extern IntPtr libvlc_media_list_player_new(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_list_player_new(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_release(IntPtr p_ml_player)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_release(IntPtr p_ml_player);
        #endregion
        #region internal static extern void libvlc_media_list_player_set_media_player(IntPtr p_ml_player, IntPtr p_media_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_media_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_set_media_player(IntPtr p_ml_player, IntPtr p_media_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_set_media_list(IntPtr p_ml_player, IntPtr p_mlist, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_mlist"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_set_media_list(IntPtr p_ml_player, IntPtr p_mlist, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_play(IntPtr p_ml_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_play(IntPtr p_ml_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_pause(IntPtr p_ml_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_pause(IntPtr p_ml_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_media_list_player_is_playing(IntPtr p_ml_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_media_list_player_is_playing(IntPtr p_ml_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern VlcState libvlc_media_list_player_get_state(IntPtr p_ml_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern VlcState libvlc_media_list_player_get_state(IntPtr p_ml_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_play_item_at_index(IntPtr p_ml_player,int i_index, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="i_index"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_play_item_at_index(IntPtr p_ml_player, int i_index, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_play_item(IntPtr p_ml_player, IntPtr p_media, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_media"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_play_item(IntPtr p_ml_player, IntPtr p_media, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_stop(IntPtr p_ml_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_stop(IntPtr p_ml_player, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_media_list_player_next(IntPtr p_ml_player, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ml_player"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_media_list_player_next(IntPtr p_ml_player, ref libvlc_exception_t p_exception);
        #endregion

        #endregion

        #region VideoLan Manager Methods
        #region internal static extern void libvlc_vlm_release(IntPtr p_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_release(IntPtr p_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_add_broadcast(IntPtr p_instance , string psz_name , string psz_input , string psz_output , int i_options , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] ppsz_options , bool b_enabled , bool b_loop , ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_input"></param>
        /// <param name="psz_output"></param>
        /// <param name="i_options"></param>
        /// <param name="ppsz_options"></param>
        /// <param name="b_enabled"></param>
        /// <param name="b_loop"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_add_broadcast(IntPtr p_instance
            , string psz_name
            , string psz_input
            , string psz_output
            , int i_options
            , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] ppsz_options
            , bool b_enabled
            , bool b_loop
            , ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_add_vod(IntPtr p_instance , string psz_name , string psz_input , int i_options , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] ppsz_options , bool b_enabled , string psz_mux , ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_input"></param>
        /// <param name="i_options"></param>
        /// <param name="ppsz_options"></param>
        /// <param name="b_enabled"></param>
        /// <param name="psz_mux"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_add_vod(IntPtr p_instance
            , string psz_name
            , string psz_input
            , int i_options
            , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] ppsz_options
            , bool b_enabled
            , string psz_mux
            , ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_del_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_del_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_set_enabled(IntPtr p_instance, string psz_name, bool b_enabled, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="b_enabled"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_set_enabled(IntPtr p_instance, string psz_name, bool b_enabled, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_set_output(IntPtr p_instance, string psz_name, string psz_output, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_output"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_set_output(IntPtr p_instance, string psz_name, string psz_output, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_set_input(IntPtr p_instance, string psz_name, string psz_input, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_input"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_set_input(IntPtr p_instance, string psz_name, string psz_input, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_add_input(IntPtr p_instance, string psz_name, string psz_input, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_input"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_add_input(IntPtr p_instance, string psz_name, string psz_input, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_set_loop(IntPtr p_instance, string psz_name, bool b_loop, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="b_loop"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_set_loop(IntPtr p_instance, string psz_name, bool b_loop, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_set_mux(IntPtr p_instancep_instance, string psz_name, string psz_mux, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instancep_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_mux"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_set_mux(IntPtr p_instancep_instance, string psz_name, string psz_mux, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_change_media(IntPtr p_instance , string psz_name , string psz_input , string psz_output , int i_options , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] ppsz_options , bool b_enabled , bool b_loop , ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="psz_input"></param>
        /// <param name="psz_output"></param>
        /// <param name="i_options"></param>
        /// <param name="ppsz_options"></param>
        /// <param name="b_enabled"></param>
        /// <param name="b_loop"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_change_media(IntPtr p_instance
            , string psz_name
            , string psz_input
            , string psz_output
            , int i_options
            , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] ppsz_options
            , bool b_enabled
            , bool b_loop
            , ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_play_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_play_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_stop_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_stop_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_pause_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_pause_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern void libvlc_vlm_seek_media(IntPtr p_instance, string psz_name, float f_percentage, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="f_percentage"></param>
        /// <param name="p_exception"></param>
        [DllImport("libvlc")]
        internal static extern void libvlc_vlm_seek_media(IntPtr p_instance, string psz_name, float f_percentage, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern string libvlc_vlm_show_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern string libvlc_vlm_show_media(IntPtr p_instance, string psz_name, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern float libvlc_vlm_get_media_instance_position(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern float libvlc_vlm_get_media_instance_position(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_vlm_get_media_instance_time(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_vlm_get_media_instance_time(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_vlm_get_media_instance_length(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_vlm_get_media_instance_length(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_vlm_get_media_instance_rate(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_vlm_get_media_instance_rate(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_vlm_get_media_instance_title(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_vlm_get_media_instance_title(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern int libvlc_vlm_get_media_instance_chapter(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern int libvlc_vlm_get_media_instance_chapter(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #region internal static extern bool libvlc_vlm_get_media_instance_seekable(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_instance"></param>
        /// <param name="psz_name"></param>
        /// <param name="i_instance"></param>
        /// <param name="p_exception"></param>
        /// <returns></returns>
        [DllImport("libvlc")]
        internal static extern bool libvlc_vlm_get_media_instance_seekable(IntPtr p_instance, string psz_name, int i_instance, ref libvlc_exception_t p_exception);
        #endregion
        #endregion

    }

    #endregion

}