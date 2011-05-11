using System;
using System.Windows;

namespace Vlc.DotNet.Wpf
{
    /// <summary>
    /// VlcRendererUtility class
    /// </summary>
    internal class VlcRendererUtility
    {
        public static readonly DependencyProperty MemoryUsageProperty = DependencyProperty.RegisterAttached("MemoryUsage", typeof(MemoryUsage), typeof(VlcRendererUtility), new PropertyMetadata(null));

        internal sealed class MemoryUsage
        {
            public MemoryUsage(long bytesAllocated)
            {
                myBytesAllocated = bytesAllocated;
                if (bytesAllocated > 0)
                    GC.AddMemoryPressure(myBytesAllocated);
                else
                    GC.SuppressFinalize(this);
            }

            ~MemoryUsage()
            {
                GC.RemoveMemoryPressure(myBytesAllocated);
            }

            readonly long myBytesAllocated;
        }
    }
}
