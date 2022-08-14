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
        private static IUser32LibraryWrapper _instance;
        public static IUser32LibraryWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new User32LibraryWrapper();
                }

                return _instance;
            }
        }

        private User32LibraryWrapper() { }

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
