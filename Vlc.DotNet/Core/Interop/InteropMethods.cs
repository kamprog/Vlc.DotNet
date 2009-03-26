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
            if (wasRaised) throw new Exception(message); //TODO: create VlcException class
        }

        public bool HadException()
        {
            bool wasRaised = InteropMethods.libvlc_exception_raised(ref this) == 1;
            InteropMethods.libvlc_exception_clear(ref this);
            return wasRaised;
        }
    }

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

    internal enum VlcState
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

    internal enum libvlc_meta_t
    {
        libvlc_meta_Title,
        libvlc_meta_Artist,
        libvlc_meta_Genre,
        libvlc_meta_Copyright,
        libvlc_meta_Album,
        libvlc_meta_TrackNumber,
        libvlc_meta_Description,
        libvlc_meta_Rating,
        libvlc_meta_Date,
        libvlc_meta_Setting,
        libvlc_meta_URL,
        libvlc_meta_Language,
        libvlc_meta_NowPlaying,
        libvlc_meta_Publisher,
        libvlc_meta_EncodedBy,
        libvlc_meta_ArtworkURL,
        libvlc_meta_TrackID
    }

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
        internal static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv, ref libvlc_exception_t exception);

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

        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_new(IntPtr p_instance, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_new_from_media(IntPtr p_media, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_release(IntPtr p_media);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_retain(IntPtr p_media);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_media(IntPtr p_media_player, IntPtr p_media, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_get_media(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_event_manager(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_play(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_pause(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_stop(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_drawable(IntPtr p_media_player, IntPtr handle, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern IntPtr libvlc_media_player_get_drawable(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern long libvlc_media_player_get_length(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern long libvlc_media_player_get_time(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_time(IntPtr p_media_player, long time, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern float libvlc_media_player_get_position(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_position(IntPtr p_media_player, float position, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_chapter(IntPtr p_media_player, int chapter, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_chapter(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_media_player_get_chapter_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_will_play(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern float libvlc_media_player_get_rate(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_media_player_set_rate(IntPtr p_media_player, float rate, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern VlcState libvlc_media_player_get_state(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern float libvlc_media_player_get_fps(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_has_vout(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_is_seekable(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern bool libvlc_media_player_can_pause(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        #endregion

        #region Video Methods

        [DllImport("libvlc")]
        internal static extern void libvlc_toggle_fullscreen(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_set_fullscreen(IntPtr p_media_player, bool b_fullscreen, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern bool libvlc_get_fullscreen(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_height(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_width(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern String libvlc_video_get_aspect_ratio(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_aspect_ratio(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_spu(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_spu(IntPtr p_media_player, int i_spu, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_video_set_subtitle_file(IntPtr p_media_player, String psz_subtitle, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern String libvlc_video_get_crop_geometry(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_crop_geometry(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_toggle_teletext(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_video_get_teletext(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_teletext(IntPtr p_media_player, int i_page, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_take_snapshot(IntPtr p_media_player, [MarshalAs(UnmanagedType.LPStr)] string psz_filepath, uint i_width, uint i_height, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_resize(IntPtr p_media_player, int width, int height, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_video_reparent(IntPtr p_media_player, IntPtr parent, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_video_set_size(IntPtr p_instance, int width, int height, ref libvlc_exception_t p_exception);

        #endregion

        #region Audio Methods

        [DllImport("libvlc")]
        internal static extern void libvlc_audio_toggle_mute(IntPtr p_instance, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern bool libvlc_audio_get_mute(IntPtr p_instance, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_mute(IntPtr p_instance, bool status, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_volume(IntPtr p_instance, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_volume(IntPtr p_instance, int i_volume, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_track_count(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_track(IntPtr p_media_player, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_track(IntPtr p_media_player, int i_track, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern int libvlc_audio_get_channel(IntPtr p_instance, ref libvlc_exception_t p_exception);

        [DllImport("libvlc")]
        internal static extern void libvlc_audio_set_channel(IntPtr p_instance, int i_channel, ref libvlc_exception_t p_exception);

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
        /// <param name="i_index"></param>
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
                                                             , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ppsz_options
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
                                                       , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ppsz_options
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
                                                            , [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ppsz_options
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

        [DllImport("plugins\\libd3dimage_plugin.dll")]
        internal static extern IntPtr libd3d_surface_changed_callback(D3dCallback f_callback);
    }
}