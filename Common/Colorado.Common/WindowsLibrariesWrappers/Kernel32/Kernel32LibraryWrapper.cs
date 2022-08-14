using System;

namespace Colorado.Common.WindowsLibrariesWrappers.Kernel32
{
    public interface IKernel32LibraryWrapper
    {
        IntPtr LoadLibrary(string libraryName);
    }

    public class Kernel32LibraryWrapper : IKernel32LibraryWrapper
    {
        private static IKernel32LibraryWrapper _instance;
        public static IKernel32LibraryWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Kernel32LibraryWrapper();
                }

                return _instance;
            }
        }

        private Kernel32LibraryWrapper() { }

        public IntPtr LoadLibrary(string libraryName)
        {
            return Kernel32LibraryAPI.LoadLibrary(libraryName);
        }
    }
}
