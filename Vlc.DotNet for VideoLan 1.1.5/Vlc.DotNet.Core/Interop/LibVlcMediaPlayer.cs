using System;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interop
{
    internal static partial class LibVlcMethods
    {
        #region LibVLC media player

        #region libvlc_navigate_mode_t enum

        public enum libvlc_navigate_mode_t
        {
            libvlc_navigate_activate = 0,
            libvlc_navigate_up,
            libvlc_navigate_down,
            libvlc_navigate_left,
            libvlc_navigate_right
        }

        #endregion

        #region libvlc_video_marquee_option_t enum

        public enum libvlc_video_marquee_option_t
        {
            libvlc_marquee_Enable = 0,
            libvlc_marquee_Text,
            libvlc_marquee_Color,
            libvlc_marquee_Opacity,
            libvlc_marquee_Position,
            libvlc_marquee_Refresh,
            libvlc_marquee_Size,
            libvlc_marquee_Timeout,
            libvlc_marquee_X,
            libvlc_marquee_Y
        }

        #endregion

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_new(IntPtr instance);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_release(IntPtr player);

        // libvlc_media_player_retain

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_media(IntPtr player, IntPtr media);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_get_media(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr libvlc_media_player_event_manager(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_is_playing(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_play(IntPtr player);

        // libvlc_media_player_set_pause 

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_pause(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_stop(IntPtr player);

        // libvlc_video_set_callbacks
        // libvlc_video_set_format
        // libvlc_media_player_set_nsobject 
        // libvlc_media_player_get_nsobject 
        // libvlc_media_player_set_agl 
        // libvlc_media_player_get_agl 
        // libvlc_media_player_set_xwindow
        // libvlc_media_player_get_xwindow 

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_hwnd(IntPtr player, IntPtr drawable);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_get_hwnd(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_media_player_get_length(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_media_player_get_time(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_time(IntPtr player, Int64 time);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_media_player_get_position(IntPtr player, Int64 time);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_position(IntPtr player, float pos);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_chapter(IntPtr player, int chapter);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_chapter(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_chapter_count(IntPtr player);

        // libvlc_media_player_will_play

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_chapter_count_for_title(IntPtr player, int title);

        /// <summary>
        /// Set movie title
        /// </summary>
        /// <param name="player">The Media Player</param>
        /// <param name="title">Title number to play</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_title(IntPtr player, int title);

        /// <summary>
        /// Get movie title
        /// </summary>
        /// <param name="player">The Media Player</param>
        /// <returns>Title number currently playing, or -1</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_title(IntPtr player);

        // libvlc_media_player_get_title_count

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_previous_chapter(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_next_chapter(IntPtr player);

        // libvlc_media_player_get_rate
        // libvlc_media_player_set_rate

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern libvlc_state_t libvlc_media_player_get_state(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_media_player_get_fps(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint libvlc_media_player_has_vout(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_is_seekable(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_can_pause(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_next_frame(IntPtr player);

        // libvlc_media_player_navigate

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_track_description_release(IntPtr ptrack);

        #region Nested type: libvlc_audio_output_t

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_audio_output_t
        {
            [MarshalAs(UnmanagedType.LPStr)] public string psz_name;
            [MarshalAs(UnmanagedType.LPStr)] public string psz_description;
            public IntPtr p_next;
        }

        #endregion

        #region Nested type: libvlc_rectangle_t

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_rectangle_t
        {
            public int top, left;
            public int bottom, right;
        }

        #endregion

        #region Nested type: libvlc_track_description_t

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_track_description_t
        {
            public uint i_id;
            [MarshalAs(UnmanagedType.LPStr)] public string psz_name;
            public IntPtr p_next;
        }

        #endregion

        #endregion

        #region LibVLC video controls

        #region libvlc_video_adjust_option_t enum

        public enum libvlc_video_adjust_option_t
        {
            libvlc_adjust_Enable = 0,
            libvlc_adjust_Contrast,
            libvlc_adjust_Brightness,
            libvlc_adjust_Hue,
            libvlc_adjust_Saturation,
            libvlc_adjust_Gamma
        }

        #endregion

        #region libvlc_video_logo_option_t enum

        public enum libvlc_video_logo_option_t
        {
            libvlc_logo_enable,
            libvlc_logo_file,
            libvlc_logo_x,
            libvlc_logo_y,
            libvlc_logo_delay,
            libvlc_logo_repeat,
            libvlc_logo_opacity,
            libvlc_logo_position
        }

        #endregion

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_toggle_fullscreen(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_set_fullscreen(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_get_fullscreen(IntPtr player);

        // libvlc_video_set_key_input
        // libvlc_video_set_mouse_input

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_size(IntPtr player, uint num, ref uint x, ref uint y);

        // libvlc_video_get_height
        // libvlc_video_get_width
        // libvlc_video_get_cursor

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_video_get_scale(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_scale(IntPtr player, float factor);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern String libvlc_video_get_aspect_ratio(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_aspect_ratio(IntPtr player, string aspect);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_spu(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_spu_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        public static extern IntPtr libvlc_video_get_spu_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_set_spu(IntPtr player, uint i_spu);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_set_subtitle_file(IntPtr player, string subtitle_file);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_get_title_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_get_chapter_description(IntPtr player, int title);

        // libvlc_video_get_crop_geometry
        // libvlc_video_set_crop_geometry
        // libvlc_video_get_teletext
        // libvlc_video_set_teletext
        // libvlc_toggle_teletext

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_track_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_get_track_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_track(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_track(IntPtr player, int track);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_take_snapshot(IntPtr player, uint num, string path, uint width, uint height);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_deinterlace(IntPtr player, string mode);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_marquee_int(IntPtr player, libvlc_video_marquee_option_t option);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_marquee_string(IntPtr player, uint option);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_marquee_int(IntPtr player, uint option, int value);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_marquee_string(IntPtr player, uint option, string value);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_logo_int(IntPtr player, uint option);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_logo_int(IntPtr player, uint option, int value);

        // libvlc_video_get_adjust_int
        // libvlc_video_set_adjust_int
        // libvlc_video_get_adjust_float
        // libvlc_video_set_adjust_float

        #endregion

        #region LibVLC audio controls

        #region libvlc_audio_output_channel_t enum

        public enum libvlc_audio_output_channel_t
        {
            libvlc_AudioChannel_Error = -1,
            libvlc_AudioChannel_Stereo = 1,
            libvlc_AudioChannel_RStereo = 2,
            libvlc_AudioChannel_Left = 3,
            libvlc_AudioChannel_Right = 4,
            libvlc_AudioChannel_Dolbys = 5
        }

        #endregion

        #region libvlc_audio_output_device_types_t enum

        public enum libvlc_audio_output_device_types_t
        {
            libvlc_AudioOutputDevice_Error = -1,
            libvlc_AudioOutputDevice_Mono = 1,
            libvlc_AudioOutputDevice_Stereo = 2,
            libvlc_AudioOutputDevice_2F2R = 4,
            libvlc_AudioOutputDevice_3F2R = 5,
            libvlc_AudioOutputDevice_5_1 = 6,
            libvlc_AudioOutputDevice_6_1 = 7,
            libvlc_AudioOutputDevice_7_1 = 8,
            libvlc_AudioOutputDevice_SPDIF = 10
        }

        #endregion

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_output_list_get(IntPtr instance);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_output_list_release(IntPtr list);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_output_set(IntPtr player, string name);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_output_device_count(IntPtr instance, string audioOutput);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_output_device_longname(IntPtr instance, string audioOutput, int device_id);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_output_device_id(IntPtr instance, string audioOutput, int device_id);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_output_device_set(IntPtr player, string audioOutput, string device_id);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_output_get_device_type(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_output_set_device_type(IntPtr player, int device_type);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_toggle_mute(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_mute(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_set_mute(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_volume(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_set_volume(IntPtr player, int newvolume);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_track_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_get_track_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_track(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_set_track(IntPtr player, int track);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_channel(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_set_channel(IntPtr player, int channel);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_audio_get_delay(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_set_delay(IntPtr player, Int64 delay);

        #endregion
    }
}