using System;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interop.Event
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void VlcCallback(ref VlcCallbackArgs Event, IntPtr UserData);

    [StructLayout(LayoutKind.Explicit)]
    internal struct VlcCallbackArgs
    {
        [FieldOffset(0)]
        public VlcEventType EventType;

        [FieldOffset(8)]
        public IntPtr Sender;

        [FieldOffset(16)]
        public media_meta_changed media_meta_changed;

        [FieldOffset(16)]
        public media_subitem_added media_subitem_added;

        [FieldOffset(16)]
        public media_duration_changed media_duration_changed;

        [FieldOffset(16)]
        public media_preparsed_changed media_preparsed_changed;

        [FieldOffset(16)]
        public media_freed media_freed;

        [FieldOffset(16)]
        public media_state_changed media_state_changed;

        [FieldOffset(16)]
        public media_player_position_changed media_player_position_changed;

        [FieldOffset(16)]
        public media_player_time_changed media_player_time_changed;

        [FieldOffset(16)]
        public media_player_seekable_changed media_player_seekable_changed;

        [FieldOffset(16)]
        public media_player_pausable_changed media_player_pausable_changed;

        [FieldOffset(16)]
        public media_list_item_added media_list_item_added;

        [FieldOffset(16)]
        public media_list_will_add_item media_list_will_add_item;

        [FieldOffset(16)]
        public media_list_item_deleted media_list_item_deleted;

        [FieldOffset(16)]
        public media_list_will_delete_item media_list_will_delete_item;

        [FieldOffset(16)]
        public media_list_view_item_added media_list_view_item_added;

        [FieldOffset(16)]
        public media_list_view_will_add_item media_list_view_will_add_item;

        [FieldOffset(16)]
        public media_list_view_item_deleted media_list_view_item_deleted;

        [FieldOffset(16)]
        public media_list_view_will_delete_item media_list_view_will_delete_item;

        [FieldOffset(16)]
        public media_player_snapshot_taken media_player_snapshot_taken;

        //[FieldOffset(16)]
        //public media_media_discoverer_started media_media_discoverer_started;

        //[FieldOffset(16)]
        //public media_media_discoverer_ended media_media_discoverer_ended;
    }

    #region media descriptor

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_meta_changed
    {
        libvlc_meta_t meta_type;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_subitem_added
    {
        IntPtr p_media;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_duration_changed
    {
        public long new_duration;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct media_preparsed_changed
    {
        public int new_status;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_freed
    {
        public IntPtr p_media;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_state_changed
    {
        public VlcState new_state;
    }

    #endregion

    #region media player

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_player_position_changed
    {
        public float new_position;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_player_time_changed
    {
        public long new_time;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_player_seekable_changed
    {
        public long new_seekable;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_player_pausable_changed
    {
        public long new_pausable;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_player_snapshot_taken
    {
        //[MarshalAs(UnmanagedType.LPStr)]
        public IntPtr psz_filename;
    }
    #endregion

    #region media list
    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_item_added
    {
        public IntPtr p_media;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_will_add_item
    {
        public IntPtr p_media;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_item_deleted
    {
        public IntPtr p_media;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_will_delete_item
    {
        public IntPtr p_media;
        public int index;
    }
    #endregion

    #region media list view
    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_view_item_added
    {
        public IntPtr p_media;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_view_will_add_item
    {
        public IntPtr p_media;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_view_item_deleted
    {
        public IntPtr p_media;
        public int index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_list_view_will_delete_item
    {
        public IntPtr p_media;
        public int index;
    }
    #endregion

    #region media discoverer
    [StructLayout(LayoutKind.Sequential)]
    internal struct media_media_discoverer_started
    {
        public IntPtr unused;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct media_media_discoverer_ended
    {
        public IntPtr unused;
    }
    #endregion
}