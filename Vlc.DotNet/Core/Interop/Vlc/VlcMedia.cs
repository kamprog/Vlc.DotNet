using System;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcMedia : IDisposable
    {
        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        /// <summary>
        /// COM pointer to our vlc media instance
        /// </summary>
        internal IntPtr p_media;

        #region Properties

        #region public string MRL

        /// <summary>
        /// Gets the MRL of the VlcMedia
        /// </summary>
        /// <value></value>
        public string MRL
        {
            get
            {
                string rtn = InteropMethods.libvlc_media_get_mrl(p_media, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        #endregion

        #region public VlcState State

        /// <summary>
        /// 
        /// Gets the State of the VlcMedia
        /// </summary>
        /// <value></value>
        public VlcState State
        {
            get
            {
                VlcState rtn = InteropMethods.libvlc_media_get_state(p_media, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        #endregion

        #region public long Length

        /// <summary>
        /// Gets the Length of the VlcMedia
        /// </summary>
        /// <value></value>
        public long Length
        {
            get
            {
                long rtn = InteropMethods.libvlc_media_get_duration(p_media, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        #endregion

        #region public bool IsPreparsed

        /// <summary>
        /// Gets the IsPreparsed of the VlcMedia
        /// </summary>
        /// <value></value>
        public bool IsPreparsed
        {
            get
            {
                bool rtn = InteropMethods.libvlc_media_is_preparsed(p_media, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        #endregion

        #endregion

        #region Constructors/Destructors

        #region internal VlcMedia(IntPtr p_media)

        /// <summary>
        /// Initializes a new instance of the <b>VlcMedia</b> class.
        /// </summary>
        /// <param name="p_media"></param>
        internal VlcMedia(IntPtr p_media) : this(p_media, false)
        {
        }

        #endregion

        #region internal VlcMedia(IntPtr p_media, bool retain)

        /// <summary>
        /// Initializes a new instance of the <b>VlcMedia</b> class.
        /// </summary>
        /// <param name="p_media"></param>
        /// <param name="retain"></param>
        internal VlcMedia(IntPtr p_media, bool retain)
        {
            //set our Media instance pointer
            this.p_media = p_media;
            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();

            if (retain && p_media != IntPtr.Zero)
            {
                InteropMethods.libvlc_media_retain(p_media);
            }
        }

        #endregion

        #region ~VlcMedia()

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before 
        /// the <b>VlcMedia</b> is reclaimed by garbage collection.
        /// </summary>
        ~VlcMedia()
        {
            Dispose(false);
        }

        #endregion

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
            if (p_media != IntPtr.Zero)
                InteropMethods.libvlc_media_release(p_media);
            p_media = IntPtr.Zero;
        }

        #endregion

        public void AddOption(string option)
        {
            InteropMethods.libvlc_media_add_option(p_media, option, ref p_exception);
            p_exception.CheckException();
        }

        public string ReadMeta(libvlc_meta_t tag)
        {
            string rtn = InteropMethods.libvlc_media_get_meta(p_media, tag, ref p_exception);
            p_exception.CheckException();
            return rtn;
        }
    }
}