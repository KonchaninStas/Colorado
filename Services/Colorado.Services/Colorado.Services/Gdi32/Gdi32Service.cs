using Colorado.Services.Gdi32.Structures;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Colorado.Services.Gdi32
{
    public interface IGdi32Service
    {
        int ChoosePixelFormat(IntPtr deviceContextHandle, IPixelFormatDescriptor pixelFormatDescriptor);
        void SetPixelFormat(IntPtr deviceContextHandle, int pixelFormat, IPixelFormatDescriptor pixelFormatDescriptor);
        void SwapBuffers(IntPtr deviceContextHandle);
    }

    public class Gdi32Service : IGdi32Service
    {
        public static IGdi32Service Instance => Host.Instance.ServiceProvider.GetService<IGdi32Service>();

        public int ChoosePixelFormat(IntPtr deviceContextHandle, IPixelFormatDescriptor pixelFormatDescriptor)
        {
            return Gdi32LibraryAPI.ChoosePixelFormat(deviceContextHandle, pixelFormatDescriptor);
        }

        public void SetPixelFormat(IntPtr deviceContextHandle, int pixelFormat, IPixelFormatDescriptor pixelFormatDescriptor)
        {
            Gdi32LibraryAPI.SetPixelFormat(deviceContextHandle, pixelFormat, pixelFormatDescriptor);
        }

        public void SwapBuffers(IntPtr deviceContextHandle)
        {
            Gdi32LibraryAPI.SwapBuffers(deviceContextHandle);
        }
    }
}
