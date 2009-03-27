using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using Vlc.DotNet.Core.Medias;

namespace Vlc.DotNet.Core
{
    /// <summary>
    /// VlcMediaLibraryManager class
    /// </summary>
    [ContentProperty("MediaItems")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class VlcMediaLibraryManager
    {
        private readonly VlcMediaCollection mediaItems;
        private readonly VlcMediaCollection mediaList;
        private readonly VlcMediaCollection mediaRandomList;
        private bool myrandom;
        private MediaBase mySelectedMedia;

        /// <summary>
        /// VlcMediaLibraryManager constructor
        /// </summary>
        public VlcMediaLibraryManager()
        {
            mediaItems = new VlcMediaCollection();
            mediaList = new VlcMediaCollection();
            mediaRandomList = new VlcMediaCollection();
            Random = false;
            Loop = false;
        }

        /// <summary>
        /// Get/Set random playing medias
        /// </summary>
        [DefaultValue(false)]
        public bool Random
        {
            get
            {
                return myrandom;
            }
            set
            {
                myrandom = value;
                if (value)
                    CreateRandomMediaList();
                else
                    CreateMediaList();
            }
        }

        /// <summary>
        /// Get/Set loop of media items
        /// </summary>
        [DefaultValue(false)]
        public bool Loop { get; set; }

        /// <summary>
        /// List of medias
        /// </summary>
        public VlcMediaCollection MediaItems
        {
            get
            {
                return mediaItems;
            }
        }

        /// <summary>
        /// Get/Set selected media
        /// </summary>
        [Browsable(false)]
        public MediaBase SelectedMedia
        {
            get
            {
                return mySelectedMedia;
            }
            set
            {
                if (mySelectedMedia != value)
                {
                    mySelectedMedia = value;
                    OnSelectedMediaChanged();
                }
            }
        }

        public event EventHandler SelectedMediaChanged;

        private void CreateMediaList()
        {
            mediaRandomList.Clear();
            mediaList.Clear();

            foreach (MediaBase item in mediaItems)
            {
                if (SelectedMedia != null)
                {
                    if (item == SelectedMedia)
                        item.Status = MediaStates.Selected;
                    else if (mediaList.Contains(SelectedMedia))
                        item.Status = MediaStates.NotPlayed;
                    else
                        item.Status = MediaStates.Played;
                }
                else
                {
                    item.Status = MediaStates.NotPlayed;
                }
                mediaList.Add(item);
            }
        }

        private void UpdateMediaList()
        {
            if (SelectedMedia != null)
                if (!MediaItems.Contains(SelectedMedia))
                    SelectedMedia.Status = MediaStates.SelectedButDisabled;
                else
                    mediaList.Remove(SelectedMedia);

            foreach (MediaBase media in mediaList.ToArray())
            {
                if (media != SelectedMedia)
                    mediaList.Remove(media);
            }
            foreach (MediaBase item in MediaItems)
            {
                if (MediaItems.Contains(SelectedMedia))
                {
                    if (mediaList.Contains(SelectedMedia))
                        item.Status = MediaStates.NotPlayed;
                    else if (SelectedMedia == item)
                        item.Status = MediaStates.Selected;
                    else
                        item.Status = MediaStates.Played;
                    mediaList.Add(item);
                }
                else
                {
                    item.Status = MediaStates.NotPlayed;
                    mediaList.Add(item);
                }
            }
        }

        private void CreateRandomMediaList()
        {
            mediaRandomList.Clear();
            mediaList.Clear();

            int beginningIndex = 0;
            if (SelectedMedia != null)
            {
                mediaRandomList.Add(SelectedMedia);
                SelectedMedia.Status = !mediaItems.Contains(SelectedMedia) ? MediaStates.SelectedButDisabled : MediaStates.Selected;
                beginningIndex = 1;
            }


            foreach (MediaBase item in mediaItems)
            {
                if (item == SelectedMedia)
                    continue;

                int index = new Random().Next(beginningIndex, mediaRandomList.Count);
                if (item.Status != MediaStates.Disabled)
                    item.Status = MediaStates.NotPlayed;
                mediaRandomList.Insert(index, item);
            }
        }

        private void UpdateRandomMediaList()
        {
            foreach (MediaBase item in mediaRandomList)
            {
                if (!mediaItems.Contains(item))
                {
                    item.Status = SelectedMedia == null ? MediaStates.Disabled : MediaStates.SelectedButDisabled;
                }
            }

            int beginningIndex = 0;
            if (SelectedMedia != null && mediaItems.Contains(SelectedMedia))
            {
                beginningIndex = mediaRandomList.IndexOf(SelectedMedia) + 1;
            }

            for (int cpt = 0; cpt < mediaItems.Count; cpt++)
            {
                MediaBase item = mediaItems[cpt];

                if (item == SelectedMedia)
                {
                    continue;
                }
                if (!mediaRandomList.Contains(item))
                {
                    int index = new Random().Next(beginningIndex, mediaRandomList.Count);
                    mediaRandomList.Insert(index, item);

                    item.Status = MediaStates.NotPlayed;
                }
                else if (SelectedMedia != null && mediaRandomList.IndexOf(SelectedMedia) < mediaRandomList.IndexOf(item))
                    item.Status = MediaStates.NotPlayed;
                else if (SelectedMedia != null && mediaRandomList.IndexOf(SelectedMedia) > mediaRandomList.IndexOf(item))
                    item.Status = MediaStates.Played;
                else
                    item.Status = MediaStates.NotPlayed;
            }
        }

        internal void Next()
        {
            if (!Random)
                UpdateMediaList();
            else
                UpdateRandomMediaList();

            VlcMediaCollection collection = Random ? mediaRandomList : mediaList;

            if (Loop && SelectedMedia != null && collection.IndexOf(SelectedMedia) == collection.Count - 1)
            {
                collection.ToList().ForEach(m => m.Status = MediaStates.NotPlayed);
            }

            MediaBase media = collection.FirstOrDefault(m => m.Status == MediaStates.NotPlayed);

            if (media != null)
            {
                if (SelectedMedia != null)
                {
                    if (SelectedMedia.Status == MediaStates.SelectedButDisabled)
                        collection.Remove(SelectedMedia);
                    else
                        SelectedMedia.Status = MediaStates.Played;
                }
                media.Status = MediaStates.Selected;
                SelectedMedia = media;
            }
        }

        internal void Previous()
        {
            if (!Random)
                UpdateMediaList();
            else
                UpdateRandomMediaList();

            VlcMediaCollection collection = Random ? mediaRandomList : mediaList;

            MediaBase media;

            for (int cpt = 0; cpt < 2; cpt++)
            {
                media = collection.LastOrDefault(m => m.Status == MediaStates.Played);
                if (media != null)
                {
                    if (SelectedMedia != null)
                    {
                        if (MediaItems.Contains(SelectedMedia))
                            SelectedMedia.Status = collection.IndexOf(media) < collection.IndexOf(SelectedMedia) ? MediaStates.NotPlayed : MediaStates.Played;
                        else
                            collection.Remove(SelectedMedia);
                    }
                    media.Status = MediaStates.Selected;
                    SelectedMedia = media;
                    break;
                }
                if (!Loop)
                    break;
                if (cpt == 0)
                    collection.
                        Where(m => m.Status == MediaStates.NotPlayed).ToList().
                        ForEach(m => m.Status = MediaStates.Played);
            }
        }

        private void OnSelectedMediaChanged()
        {
            foreach (MediaBase item in mediaItems)
            {
                item.IsMediaSelected = item == SelectedMedia;
            }

            if (SelectedMediaChanged != null)
                SelectedMediaChanged(this, EventArgs.Empty);
        }

    }
}