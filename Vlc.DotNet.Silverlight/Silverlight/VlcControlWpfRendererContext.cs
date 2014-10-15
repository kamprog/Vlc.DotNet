using System;
using Vlc.DotNet.Core.Interops;

namespace Vlc.DotNet.Silverlight
{
    internal class VlcControlWpfRendererContext
    {
        public IntPtr Data { get; private set; }
        public int Size { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Stride { get; private set; }

        public VlcControlWpfRendererContext(uint width, uint height)
            : this((int)width, (int)height)
        {

        }
        public VlcControlWpfRendererContext(double width, double height)
            : this((int)width, (int)height)
        {
        }
        public VlcControlWpfRendererContext(int width, int height)
        {
            Size = width * height * 32 / 8;
            Data = Win32Interop.LocalAlloc(0, (IntPtr)Size);
            Width = width;
            Height = height;
            Stride = width * 32 / 8;
        }
        ~VlcControlWpfRendererContext()
        {
            Win32Interop.LocalFree(Data);
        }
    }
}