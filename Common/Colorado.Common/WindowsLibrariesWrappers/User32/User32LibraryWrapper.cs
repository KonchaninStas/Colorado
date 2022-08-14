using System;

namespace Colorado.Common.WindowsLibrariesWrappers.User32
{
    public interface IUser32LibraryWrapper
    {
        IntPtr GetDeviceContext(IntPtr windowHandle);
        int ReleaseDeviceContext(IntPtr windowHandle, IntPtr deviceContextHandle);
    }

    public class User32LibraryWrapper : IUser32LibraryWrapper
    {
        public User32LibraryWrapper() { }

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
