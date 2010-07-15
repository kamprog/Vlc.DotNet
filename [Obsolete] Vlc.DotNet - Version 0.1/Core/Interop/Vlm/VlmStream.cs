namespace Vlc.DotNet.Core.Interop.Vlm
{
    internal abstract class VlmStream
    {
        private readonly string _Name;

        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        internal libvlc_exception_t p_exception;

        protected VideoLanManager Vlm;

        internal VlmStream(VideoLanManager Vlm, string Name)
        {
            this.Vlm = Vlm;
            _Name = Name;

            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
        }

        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public int Length
        {
            get
            {
                int rtn = InteropMethods.libvlc_vlm_get_media_instance_length(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public int Time
        {
            get
            {
                int rtn = InteropMethods.libvlc_vlm_get_media_instance_time(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public float Position
        {
            get
            {
                float rtn = InteropMethods.libvlc_vlm_get_media_instance_position(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public int Chapter
        {
            get
            {
                int rtn = InteropMethods.libvlc_vlm_get_media_instance_chapter(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public int Rate
        {
            get
            {
                int rtn = InteropMethods.libvlc_vlm_get_media_instance_rate(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public bool CanSeek
        {
            get
            {
                bool rtn = InteropMethods.libvlc_vlm_get_media_instance_seekable(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public int Title
        {
            get
            {
                int rtn = InteropMethods.libvlc_vlm_get_media_instance_title(Vlm.Vlc.p_instance, Name, 0, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public string Description
        {
            get
            {
                string rtn = InteropMethods.libvlc_vlm_show_media(Vlm.Vlc.p_instance, Name, ref p_exception);
                p_exception.CheckException();
                return rtn;
            }
        }

        public void Play()
        {
            InteropMethods.libvlc_vlm_play_media(Vlm.Vlc.p_instance, Name, ref p_exception);
            p_exception.CheckException();
        }

        public void Pause()
        {
            InteropMethods.libvlc_vlm_pause_media(Vlm.Vlc.p_instance, Name, ref p_exception);
            p_exception.CheckException();
        }

        public void Stop()
        {
            InteropMethods.libvlc_vlm_stop_media(Vlm.Vlc.p_instance, Name, ref p_exception);
            p_exception.CheckException();
        }

        public void SeekTo(float position)
        {
            InteropMethods.libvlc_vlm_seek_media(Vlm.Vlc.p_instance, Name, position, ref p_exception);
            p_exception.CheckException();
        }
    }
}