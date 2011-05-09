using System;
using System.Runtime.InteropServices;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.Media;

namespace Vlc.DotNet.Core.Interops.Signatures
{
    namespace LibVlc
    {
        namespace MediaPlayer
        {
            public class TrackDescription
            {
                public int id;
                public string name;
                public TrackDescription Next;
            }

            public class AudioOutput
            {
                public string name;
                public string description;
                public AudioOutput next;
            }

            public struct Rectangle
            {
                public int top, left, bottom, right;
            }

            public enum VideoMarqueeOption
            {
                Enable = 0,
                Text,
                Color,
                Opacity,
                Position,
                Refresh,
                Size,
                Timeout,
                X,
                Y
            }

            public enum NavigationMode
            {
                Activate = 0,
                Up,
                Down,
                Left,
                Right
            }

            /// <summary>
            /// Create an empty Media Player object
            /// </summary>
            /// <param name="vlcInstance">The libvlc instance in which the Media Player</param>
            /// <returns>A new media player object, or NULL on error.</returns>
            [LibVlcFunction("libvlc_media_player_new")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr NewInstance(IntPtr vlcInstance);

            /// <summary>
            /// Create a Media Player object from a Media
            /// </summary>
            /// <param name="mediaInstance">The media. Afterwards the p_md can be safely destroyed.</param>
            /// <returns>A new media player object, or NULL on error.</returns>
            [LibVlcFunction("libvlc_media_player_new_from_media")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr NewInstanceFromMedia(IntPtr mediaInstance);

            /// <summary>
            /// Release a media_player after use decrement the reference count of a media player object. If the reference count is 0, then libvlc_media_player_release() will release the media player object. If the media player object has been released, then it should not be used again.
            /// </summary>
            /// <param name="playerInstance">The Media Player to free</param>
            [LibVlcFunction("libvlc_media_player_release")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void ReleaseInstance(IntPtr playerInstance);

            /// <summary>
            /// Retain a reference to a media player object. Use libvlc_media_player_release() to decrement reference count.
            /// </summary>
            /// <param name="playerInstance">Media player object</param>
            [LibVlcFunction("libvlc_media_player_retain")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void RetainInstance(IntPtr playerInstance);

            /// <summary>
            /// Set the media that will be used by the media_player. If any, previous md will be released.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="media">The Media. Afterwards the media can be safely destroyed.</param>
            [LibVlcFunction("libvlc_media_player_set_media")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetMedia(IntPtr playerInstance, IntPtr media);

            /// <summary>
            /// Get the media used by the media_player.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>The media associated with playerInstance, or NULL if no media is associated</returns>
            [LibVlcFunction("libvlc_media_player_get_media")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr GetMedia(IntPtr playerInstance);

            /// <summary>
            /// Get the Event Manager from which the media player send event.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>The event manager associated with playerInstance</returns>
            [LibVlcFunction("libvlc_media_player_event_manager")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr EventManager(IntPtr playerInstance);

            /// <summary>
            /// Is playing
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>1 if the media player is playing, 0 otherwise</returns>
            [LibVlcFunction("libvlc_media_player_is_playing")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int IsPlaying(IntPtr playerInstance);

            /// <summary>
            /// Play
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>0 if playback started (and was already started), or -1 on error.</returns>
            [LibVlcFunction("libvlc_media_player_play")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int Play(IntPtr playerInstance);

            /// <summary>
            /// Pause or resume (no effect if there is no media)
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="pause">play/resume if zero, pause if non-zero</param>
            [LibVlcFunction("libvlc_media_player_set_pause", "1.1.1")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetPause(IntPtr playerInstance, int pause);

            /// <summary>
            /// Toggle pause (no effect if there is no media)
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            [LibVlcFunction("libvlc_media_player_pause")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void Pause(IntPtr playerInstance);

            /// <summary>
            /// Stop (no effect if there is no media)
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            [LibVlcFunction("libvlc_media_player_stop")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void Stop(IntPtr playerInstance);

            namespace Video
            {
                /// <summary>
                /// Callback prototype to allocate and lock a picture buffer. Whenever a new video frame needs to be decoded, the lock callback is invoked. Depending on the video chroma, one or three pixel planes of adequate dimensions must be returned via the second parameter. Those planes must be aligned on 32-bytes boundaries.
                /// </summary>
                /// <param name="opaque">Private pointer as passed to SetCallbacks()</param>
                /// <param name="planes">Planes start address of the pixel planes (LibVLC allocates the array of void pointers, this callback must initialize the array)</param>
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void LockCallbackDelegate(IntPtr opaque, ref IntPtr planes);

                /// <summary>
                /// Callback prototype to unlock a picture buffer. When the video frame decoding is complete, the unlock callback is invoked. This callback might not be needed at all. It is only an indication that the application can now read the pixel values if it needs to.
                /// </summary>
                /// <param name="opaque">Private pointer as passed to SetCallbacks()</param>
                /// <param name="picture">Private pointer returned from the LockCallback callback</param>
                /// <param name="planes">Pixel planes as defined by the @ref libvlc_video_lock_cb callback (this parameter is only for convenience)</param>
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void UnlockCallbackDelegate(IntPtr opaque, IntPtr picture, ref IntPtr planes);

                /// <summary>
                /// Callback prototype to display a picture. When the video frame needs to be shown, as determined by the media playback clock, the display callback is invoked.
                /// </summary>
                /// <param name="opaque">Private pointer as passed to SetCallbacks()</param>
                /// <param name="picture">Private pointer returned from the LockCallback callback</param>
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void DisplayCallbackDelegate(IntPtr opaque, IntPtr picture);

                [LibVlcFunction("libvlc_video_set_callbacks", "1.1.1")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void SetCallbacks(IntPtr playerInstance, LockCallbackDelegate @lock, UnlockCallbackDelegate unlock, DisplayCallbackDelegate display, IntPtr opaque);

                [LibVlcFunction("libvlc_video_set_format", "1.1.1")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void SetFormat(IntPtr playerInstance, string chroma, uint width, uint height, uint pitch);

                /// <summary>
                /// Callback prototype to configure picture buffers format. This callback gets the format of the video as output by the video decoder and the chain of video filters (if any). It can opt to change any parameter as it needs. In that case, LibVLC will attempt to convert the video format (rescaling and chroma conversion) but these operations can be CPU intensive.
                /// </summary>
                /// <param name="opaque">Pointer to the private pointer passed to SetCallbacks()</param>
                /// <param name="chroma">Pointer to the 4 bytes video format identifier</param>
                /// <param name="width">Pointer to the pixel width</param>
                /// <param name="height">Pointer to the pixel height</param>
                /// <param name="pitches">Table of scanline pitches in bytes for each pixel plane (the table is allocated by LibVLC)</param>
                /// <param name="lines">Table of scanlines count for each plane</param>
                /// <returns>Number of picture buffers allocated, 0 indicates failure</returns>
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate uint FormatCallbackDelegate(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines);

                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void CleanupCallbackDelegate(IntPtr opaque);

                [LibVlcFunction("libvlc_video_set_format_callbacks", "1.2.0")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void SetFormatCallbacks(IntPtr playerInstance, FormatCallbackDelegate setup, CleanupCallbackDelegate cleanup);
            }

            /// <summary>
            /// Set a Win32/Win64 API window handle (HWND) where the media player should render its video output. If LibVLC was built without Win32/Win64 API output support, then this has no effects.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="drawable">Windows handle of the drawable</param>
            [LibVlcFunction("libvlc_media_player_set_hwnd")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetHwnd(IntPtr playerInstance, IntPtr drawable);

            /// <summary>
            /// Get the Windows API window handle (HWND) previously set with SetHwnd(). The handle will be returned even if LibVLC is not currently outputting any video to it.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Window handle or NULL if there are none.</returns>
            [LibVlcFunction("libvlc_media_player_get_hwnd")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr GetHwnd(IntPtr playerInstance);

            /// <summary>
            /// Get the current movie length (in ms).
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Movie length (in ms), or -1 if there is no media.</returns>
            [LibVlcFunction("libvlc_media_player_get_length")]
            public delegate uint GetLength(IntPtr playerInstance);

            /// <summary>
            /// Get the current movie time (in ms).
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Movie time (in ms), or -1 if there is no media.</returns>
            [LibVlcFunction("libvlc_media_player_get_time")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate uint GetTime(IntPtr playerInstance);

            /// <summary>
            /// Set the movie time (in ms). This has no effect if no media is being played. Not all formats and protocols support this.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="time">The movie time (in ms).</param>
            [LibVlcFunction("libvlc_media_player_set_time")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetTime(IntPtr playerInstance, uint time);

            /// <summary>
            /// Get movie position.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Movie position, or -1. in case of error</returns>
            [LibVlcFunction("libvlc_media_player_get_position")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate float GetPosition(IntPtr playerInstance);

            /// <summary>
            /// Set movie position.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="position">Movie position</param>
            [LibVlcFunction("libvlc_media_player_set_position")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetPosition(IntPtr playerInstance, float position);

            /// <summary>
            /// Set movie chapter (if applicable).
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="chapter">Chapter number to play</param>
            [LibVlcFunction("libvlc_media_player_set_chapter")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetChapter(IntPtr playerInstance, int chapter);

            /// <summary>
            /// Get movie chapter.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Chapter number currently playing, or -1 if there is no media.</returns>
            [LibVlcFunction("libvlc_media_player_get_chapter")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int GetChapter(IntPtr playerInstance);

            /// <summary>
            /// Get movie chapter count.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Number of chapters in movie, or -1.</returns>
            [LibVlcFunction("libvlc_media_player_get_chapter_count")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int GetChapterCount(IntPtr playerInstance);

            /// <summary>
            /// Is the player able to play.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns></returns>
            [LibVlcFunction("libvlc_media_player_will_play")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int WillPlay(IntPtr playerInstance);

            /// <summary>
            /// Get title chapter count.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="title">Title</param>
            /// <returns>Number of chapters in title, or -1.</returns>
            [LibVlcFunction("libvlc_media_player_get_chapter_count_for_title")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int GetChapterCountForTitle(IntPtr playerInstance, int title);

            /// <summary>
            /// Set movie title.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="title">Title number to play</param>
            [LibVlcFunction("libvlc_media_player_set_title")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetTitle(IntPtr playerInstance, int title);

            /// <summary>
            /// Get movie title.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Title number currently playing, or -1.</returns>
            [LibVlcFunction("libvlc_media_player_get_title")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int GetTitle(IntPtr playerInstance);

            /// <summary>
            /// Get movie title count.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Title number count, or -1.</returns>
            [LibVlcFunction("libvlc_media_player_get_title_count")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int GetTitleCount(IntPtr playerInstance);

            /// <summary>
            /// Set previous chapter (if applicable)
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            [LibVlcFunction("libvlc_media_player_previous_chapter")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetPreviousChapter(IntPtr playerInstance);

            /// <summary>
            /// Set next chapter (if applicable)
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            [LibVlcFunction("libvlc_media_player_next_chapter")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void SetNextChapter(IntPtr playerInstance);

            /// <summary>
            /// Get the requested movie play rate.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Movie play rate.</returns>
            [LibVlcFunction("libvlc_media_player_get_rate")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int GetRate(IntPtr playerInstance);

            /// <summary>
            /// Set the requested movie play rate.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="rate">Rate movie play rate to set.</param>
            /// <returns>-1 if an error was detected, 0 otherwise (but even then, it might not actually work depending on the underlying media protocol)</returns>
            [LibVlcFunction("libvlc_media_player_set_rate")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int SetRate(IntPtr playerInstance, int rate);

            /// <summary>
            /// Get current movie state.
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>The current state of the media player (playing, paused, ...)</returns>
            [LibVlcFunction("libvlc_media_player_get_state")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate States GetState(IntPtr playerInstance);

            /// <summary>
            /// Get movie fps rate
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Frames per second (fps) for this playing movie, or 0 if unspecified</returns>
            [LibVlcFunction("libvlc_media_player_get_fps")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate float GetFPS(IntPtr playerInstance);

            /// <summary>
            /// How many video outputs does this media player have?
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>Number of video outputs.</returns>
            [LibVlcFunction("libvlc_media_player_has_vout")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int HasVideoOut(IntPtr playerInstance);

            /// <summary>
            /// Is this media player seekable?
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>True if the media player can seek</returns>
            [LibVlcFunction("libvlc_media_player_is_seekable")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int IsSeekable(IntPtr playerInstance);

            /// <summary>
            /// Can this media player be paused?
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <returns>True if the media player can pause</returns>
            [LibVlcFunction("libvlc_media_player_can_pause")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int IsPausable(IntPtr playerInstance);

            /// <summary>
            /// Display the next frame (if supported)
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            [LibVlcFunction("libvlc_media_player_next_frame")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void NextFrame(IntPtr playerInstance);

            /// <summary>
            /// Navigate through DVD Menu
            /// </summary>
            /// <param name="playerInstance">The Media Player</param>
            /// <param name="navigate">The Navigation mode</param>
            [LibVlcFunction("libvlc_media_player_navigate", "1.2.0")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void Navigate(IntPtr playerInstance, uint navigate);

            /// <summary>
            /// Release (free) trackDescription
            /// </summary>
            /// <param name="trackDescription">TrackDescription to release</param>
            [LibVlcFunction("libvlc_track_description_release")]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void ReleaseTrackDescription(TrackDescription trackDescription);

            namespace Video
            {
                /// <summary>
                /// Toggle fullscreen status on non-embedded video outputs.
                /// </summary>
                /// <param name="playerInstance">The Media Player</param>
                [LibVlcFunction("libvlc_toggle_fullscreen")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void ToggleFullscreen(IntPtr playerInstance);

                /// <summary>
                /// Enable or disable fullscreen.
                /// </summary>
                /// <param name="playerInstance">The Media Player</param>
                /// <param name="fullscreen">Boolean for fullscreen status</param>
                [LibVlcFunction("libvlc_set_fullscreen")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate void SetFullscreen(IntPtr playerInstance, int fullscreen);

                /// <summary>
                /// Get current fullscreen status.
                /// </summary>
                /// <param name="playerInstance">The Media Player</param>
                /// <returns>Fullscreen status (boolean)</returns>
                [LibVlcFunction("libvlc_get_fullscreen")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate int GetFullscreen(IntPtr playerInstance);




                [LibVlcFunction("libvlc_video_get_size")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate int GetSize(IntPtr playerInstance, uint num, out uint x, out uint y);

                [LibVlcFunction("libvlc_video_get_scale")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate int GetScale(IntPtr playerInstance);

                [LibVlcFunction("libvlc_video_set_scale")]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
                public delegate int SetScale(IntPtr playerInstance, float scale);
            }

            namespace Audio
            {

            }
        }
    }
}
