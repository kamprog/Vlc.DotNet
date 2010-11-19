namespace Vlc.DotNet.Core.Interop.Event
{
    internal enum VlcEventType
    {
        MediaMetaChanged,
        MediaSubItemAdded,
        MediaDurationChanged,
        MediaPreparsedChanged,
        MediaFreed,
        MediaStateChanged,

        MediaPlayerNothingSpecial,
        MediaPlayerOpening,
        MediaPlayerBuffering,
        MediaPlayerPlaying,
        MediaPlayerPaused,
        MediaPlayerStopped,
        MediaPlayerForward,
        MediaPlayerBackward,
        MediaPlayerEndReached,
        MediaPlayerEncounteredError,
        MediaPlayerTimeChanged,
        MediaPlayerPositionChanged,
        MediaPlayerSeekableChanged,
        MediaPlayerPausableChanged,

        MediaListItemAdded,
        MediaListWillAddItem,
        MediaListItemDeleted,
        MediaListWillDeleteItem,

        MediaListViewItemAdded,
        MediaListViewWillAddItem,
        MediaListViewItemDeleted,
        MediaListViewWillDeleteItem,

        MediaListPlayerPlayed,
        MediaListPlayerNextItemSet,
        MediaListPlayerStopped,

        MediaDiscovererStarted,
        MediaDiscovererEnded,
        MediaPlayerTitleChanged,
        MediaPlayerSnapshotTaken,
    }
}