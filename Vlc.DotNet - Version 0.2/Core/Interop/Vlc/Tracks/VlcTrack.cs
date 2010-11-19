namespace Vlc.DotNet.Core.Interop.Vlc.Tracks
{
    internal struct VlcTrack
    {
        private int _ID;
        private string _Name;

        public int ID { get { return _ID; } }
        public string Name { get { return _Name; } }

        public VlcTrack(int ID, string Name)
        {
            _ID = ID;
            _Name = Name;
        }
    }
}
