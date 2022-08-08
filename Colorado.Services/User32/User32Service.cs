using Microsoft.Extensions.DependencyInjection;
using System;

namespace Colorado.Services.User32
{
    public interface IUser32Service
    {
        IntPtr GetDeviceContext(IntPtr windowHandle);
        int ReleaseDeviceContext(IntPtr windowHandle, IntPtr deviceContextHandle);
    }

    public class User32Service : IUser32Service
    {
        public static IUser32Service Instance => ServiceManager.Instance.ServiceProvider.GetService<IUser32Service>();

        public int ReleaseDeviceContext(IntPtr windowHandle, IntPtr deviceContextHandle)
        {
            return User32LibraryAPI.ReleaseDC(windowHandle, deviceContextHandle);
        }

        public IntPtr GetDeviceContext(IntPtr windowHandle)
        {
            return User32LibraryAPI.GetDC(windowHandle);
        }
    }
}