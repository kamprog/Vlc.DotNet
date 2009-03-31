using System;
using System.ComponentModel;

namespace Vlc.DotNet.Core.Medias
{
    public abstract class MediaBase : INotifyPropertyChanged
    {
        private string myDescription;
        private bool myIsMediaSelected;
        private long myLength;
        private MediaStates myStatus;
        private string myTitle;
        internal Guid UniqueId = Guid.NewGuid();

        protected MediaBase()
        {
            Status = MediaStates.NotPlayed;
        }

        protected MediaBase(string prefix)
            : this()
        {
            Prefix = prefix;
            Title = "";
            Length = 0L;
        }

        internal string Prefix { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get
            {
                return myTitle;
            }
            internal set
            {
                myTitle = value;
                OnPropertyChanged("Title");
            }
        }

        public bool IsMediaSelected
        {
            get
            {
                return myIsMediaSelected;
            }
            internal set
            {
                myIsMediaSelected = value;
                OnPropertyChanged("IsMediaSelected");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long Length
        {
            get
            {
                return myLength;
            }
            internal set
            {
                myLength = value;
                OnPropertyChanged("Length");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description
        {
            get
            {
                return myDescription;
            }
            internal set
            {
                Description = value;
                OnPropertyChanged("Description");
            }
        }

        internal MediaStates Status
        {
            get
            {
                return myStatus;
            }
            set
            {
                myStatus = value;
                OnPropertyChanged("Status");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected internal abstract string RetrieveMrl();
        protected internal abstract string[] RetrieveOptions();

        public static bool operator ==(MediaBase m1, MediaBase m2)
        {
            if (Equals(m1, null))
                return (ReferenceEquals(m2, null));
            if (ReferenceEquals(m2, null))
                return (ReferenceEquals(m1, null));
            if (m1.UniqueId == m2.UniqueId)
                return true;
            return false;
        }

        public static bool operator !=(MediaBase m1, MediaBase m2)
        {
            return !(m1 == m2);
        }

        public bool Equals(MediaBase obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.UniqueId.Equals(UniqueId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MediaBase)) return false;
            return Equals((MediaBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = UniqueId.GetHashCode();
                return result;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}