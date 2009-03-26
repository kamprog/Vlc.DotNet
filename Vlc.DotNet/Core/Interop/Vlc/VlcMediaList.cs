using System;
using System.Collections;
using System.Collections.Generic;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcMediaList : IDisposable, IEnumerable<VlcMedia>
    {
        internal VideoLanClient _Vlc;

        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        /// <summary>
        /// COM pointer to our vlc media list instance
        /// </summary>
        internal IntPtr p_mlist;

        #region VlcMediaList Properties

        public int Count
        {
            get
            {
                int rtn = InteropMethods.libvlc_media_list_count(p_mlist, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return InteropMethods.libvlc_media_list_is_readonly(p_mlist);
            }
        }

        public VlcMedia this[int index]
        {
            get
            {
                IntPtr p_media = InteropMethods.libvlc_media_list_item_at_index(p_mlist, index, ref p_exception);
                p_exception.CheckException();
                return new VlcMedia(p_media, true);
            }
        }

        #endregion

        #region Constructors/Destructors

        internal VlcMediaList(VideoLanClient Vlc, IntPtr p_mlist)
        {
            _Vlc = Vlc;

            //set our Media list pointer
            this.p_mlist = p_mlist;

            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
        }

        ~VlcMediaList()
        {
            Dispose(false);
        }

        #endregion

        #region IDisposable Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            // release managed code
            if (disposing)
            {
            }

            //release unmanaged code
            if (p_mlist != IntPtr.Zero)
                InteropMethods.libvlc_media_list_release(p_mlist);
            p_mlist = IntPtr.Zero;
        }

        #endregion

        #region Methods

        public int IndexOf(VlcMedia item)
        {
            int rtn = InteropMethods.libvlc_media_list_index_of_item(p_mlist, item.p_media, ref p_exception);
            p_exception.CheckException();
            return rtn;
        }

        public void Add(VlcMedia item)
        {
            InteropMethods.libvlc_media_list_add_media(p_mlist, item.p_media, ref p_exception);
            p_exception.CheckException();
        }

        public VlcMedia AddNew(string mrl)
        {
            VlcMedia media = _Vlc.CreateMedia(mrl);
            Add(media);
            return media;
        }

        public void Insert(int index, VlcMedia item)
        {
            InteropMethods.libvlc_media_list_insert_media(p_mlist, item.p_media, index, ref p_exception);
            p_exception.CheckException();
        }

        public bool Remove(VlcMedia item)
        {
            RemoveAt(IndexOf(item));
            return true;
        }

        public void RemoveAt(int index)
        {
            InteropMethods.libvlc_media_list_remove_index(p_mlist, index, ref p_exception);
            p_exception.CheckException();
        }

        public void Lock()
        {
            InteropMethods.libvlc_media_list_lock(p_mlist);
        }

        public void Unlock()
        {
            InteropMethods.libvlc_media_list_unlock(p_mlist);
        }

        #endregion

        #region IEnumerable<VlcMedia> Members

        public IEnumerator<VlcMedia> GetEnumerator()
        {
            return new VlcMediaEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new VlcMediaEnumerator(this);
        }

        #endregion

        #region Custom Enumerator

        private class VlcMediaEnumerator : IEnumerator<VlcMedia>
        {
            private readonly VlcMediaList _mlist;
            private int _index;

            #region Properties

            public object Current
            {
                get
                {
                    return _mlist[_index];
                }
            }

            VlcMedia IEnumerator<VlcMedia>.Current
            {
                get
                {
                    return _mlist[_index];
                }
            }

            #endregion

            #region Constructors/Destructors

            public VlcMediaEnumerator(VlcMediaList mlist)
            {
                _mlist = mlist;
                _index = -1;
            }

            #endregion

            #region IEnumerator<VlcMedia> Members

            public void Reset()
            {
                _index = -1;
            }

            public bool MoveNext()
            {
                return (++_index >= _mlist.Count);
            }

            public void Dispose()
            {
            }

            #endregion

            #endregion
        }
    }
}