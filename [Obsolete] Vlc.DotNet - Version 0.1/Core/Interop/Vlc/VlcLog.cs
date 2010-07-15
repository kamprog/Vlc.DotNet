using System;
using System.Collections;
using System.Collections.Generic;

namespace Vlc.DotNet.Core.Interop.Vlc
{
    internal class VlcLog : IDisposable, IEnumerable<VlcLogMessage>
    {
        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        /// <summary>
        /// COM pointer to our vlc media instance
        /// </summary>
        internal IntPtr p_log;

        #region Properties

        public uint Count
        {
            get
            {
                uint rtn = InteropMethods.libvlc_log_count(p_log, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        #endregion

        #region Constructors/Destructors

        internal VlcLog(IntPtr p_log)
        {
            this.p_log = p_log;
        }

        ~VlcLog()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

        #region VlcLog Methods

        public void Clear()
        {
            InteropMethods.libvlc_log_clear(p_log, ref p_exception);
            p_exception.CheckException();
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
            if (p_log != IntPtr.Zero)
            {
                InteropMethods.libvlc_log_close(p_log, ref p_exception);
                p_exception.CheckException();
            }
            p_log = IntPtr.Zero;
        }

        #endregion

        #region IEnumerable<VlcLogMessage> Methods

        public IEnumerator<VlcLogMessage> GetEnumerator()
        {
            return new VlcLogEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new VlcLogEnumerator(this);
        }

        #region Custom Enumerator

        private class VlcLogEnumerator : IEnumerator<VlcLogMessage>
        {
            private readonly VlcLog _log;
            private VlcLogMessage _cur_msg;

            /// <summary>
            /// COM pointer to our vlc media instance
            /// </summary>
            internal IntPtr p_log_iter;

            #region Properties

            public object Current
            {
                get
                {
                    return _cur_msg;
                }
            }

            VlcLogMessage IEnumerator<VlcLogMessage>.Current
            {
                get
                {
                    return _cur_msg;
                }
            }

            #endregion

            #region Constructors/Destructors

            public VlcLogEnumerator(VlcLog log)
            {
                _log = log;
                Initalize();
            }

            ~VlcLogEnumerator()
            {
                Dispose(false);
            }

            private void Initalize()
            {
                //Get the message iterator
                p_log_iter = InteropMethods.libvlc_log_get_iterator(_log.p_log, ref _log.p_exception);
                _log.p_exception.CheckException();

                //Allocate the first message
                MoveNext();
            }

            #endregion

            #region IEnumerator<VlcLogMessage> Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void Reset()
            {
                Dispose(false);
                _cur_msg = null;
                Initalize();
            }

            public bool MoveNext()
            {
                bool rtn = InteropMethods.libvlc_log_iterator_has_next(p_log_iter, ref _log.p_exception);
                _log.p_exception.CheckException();

                if (rtn)
                {
                    _cur_msg = new VlcLogMessage();
                    InteropMethods.libvlc_log_iterator_next(p_log_iter, ref _cur_msg.msg, ref _log.p_exception);
                    _log.p_exception.CheckException();
                }
                return rtn;
            }

            #endregion

            protected void Dispose(bool disposing)
            {
                // release managed code
                if (disposing)
                {
                }

                //release unmanaged code
                if (p_log_iter != IntPtr.Zero)
                {
                    InteropMethods.libvlc_log_iterator_free(p_log_iter, ref _log.p_exception);
                    _log.p_exception.CheckException();
                }
                p_log_iter = IntPtr.Zero;
            }
        }

        #endregion

        #endregion

        #endregion
    }
}