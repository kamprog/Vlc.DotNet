using System;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Vlc.DotNet.Wpf
{
    internal struct VlcControlWpfRendererContext
    {
        public IntPtr Data;
        public int Size;

        public VlcControlWpfRendererContext(uint width, uint height, PixelFormat format)
            : this((int)width, (int)height, format)
        {
            
        }
        public VlcControlWpfRendererContext(double width, double height, PixelFormat format)
            : this((int)width, (int)height, format)
        {
        }

        public VlcControlWpfRendererContext(int width, int height, PixelFormat format)
        {
            Size = width * height * format.BitsPerPixel / 8;
            Data = Marshal.AllocHGlobal(Size);
            Width = width;
            Height = height;
            PixelFormat = format;
            Stride = width * format.BitsPerPixel / 8;
        }

        public int Width;
        public int Height;
        public int Stride;
        public PixelFormat PixelFormat;
    }
}