using System.Collections;
using System.Collections.Specialized;

namespace Vlc.DotNet.Core.Interop.Vlm
{
    internal class VlmStreamCollection<T> : IEnumerable
        where T : VlmStream
    {
        private readonly OrderedDictionary Streams;

        #region Properties

        public T this[int index]
        {
            get
            {
                return Streams[index] as T;
            }
        }

        public T this[string key]
        {
            get
            {
                return Streams[key] as T;
            }
        }

        public int Count
        {
            get
            {
                return Streams.Count;
            }
        }

        #endregion

        #region Constructors/Destructors

        internal VlmStreamCollection()
        {
            Streams = new OrderedDictionary();
        }

        #endregion

        #region Methods

        public bool Contains(string key)
        {
            return Streams.Contains(key);
        }

        //Internal methods.  This is what makes it a read-only list
        internal void Add(string key, T value)
        {
            Streams.Add(key, value);
        }

        internal void Clear()
        {
            Streams.Clear();
        }

        internal void Remove(string key)
        {
            Streams.Remove(key);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Streams.GetEnumerator();
        }

        #endregion
    }
}