using System.Collections;

namespace Vlc.DotNet.Core
{
    public class StringEnumerator
    {
        private readonly IEnumerator myBaseEnumerator;
        private readonly IEnumerable myTemp;

        internal StringEnumerator(StringCollection mappings)
        {
            myTemp = mappings;
            myBaseEnumerator = myTemp.GetEnumerator();
        }

        public string Current
        {
            get { return (string) myBaseEnumerator.Current; }
        }

        public bool MoveNext()
        {
            return myBaseEnumerator.MoveNext();
        }

        public void Reset()
        {
            myBaseEnumerator.Reset();
        }
    }
}