using System;
using System.Collections.Generic;
using System.Text;

namespace Vlc.DotNet.Core
{
    public enum State : int
    {
        NothingSpecial = 0,
		Opening = 1,
		Buffering = 2,
		Playing = 3,
		Paused = 4,
		Stopped = 5,
		Ended = 6,
		Error = 7
    }
}
