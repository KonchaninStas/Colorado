using Colorado.Common.WindowsLibrariesWrappers.Gdi32.Structures;
using System;

namespace Colorado.Common.WindowsLibrariesWrappers.Gdi32
{
    public interface IGdi32LibraryWrapper
    {
        int ChoosePixelFormat(IntPtr deviceContextHandle, PixelFormatDescriptor pixelFormatDescriptor);
        void SetPixelFormat(IntPtr deviceContextHandle, int pixelFormat, PixelFormatDescriptor pixelFormatDescriptor);
        void SwapBuffers(IntPtr deviceContextHandle);
    }

    public class Gdi32LibraryWrapper : IGdi32LibraryWrapper
    {
        public Gdi32LibraryWrapper() { }

        public int ChoosePixelFormat(IntPtr deviceContextHandle, PixelFormatDescriptor pixelFormatDescriptor)
        {
            return Gdi32LibraryAPI.ChoosePixelFormat(deviceContextHandle, pixelFormatDescriptor);
        }

        public void SetPixelFormat(IntPtr deviceContextHandle, int pixelFormat, PixelFormatDescriptor pixelFormatDescriptor)
        {
            Gdi32LibraryAPI.SetPixelFormat(deviceContextHandle, pixelFormat, pixelFormatDescriptor);
        }

        public void SwapBuffers(IntPtr deviceContextHandle)
        {
            Gdi32LibraryAPI.SwapBuffers(deviceContextHandle);
        }
    }
}
