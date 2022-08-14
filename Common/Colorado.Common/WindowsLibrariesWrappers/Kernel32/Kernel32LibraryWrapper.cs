using System;

namespace Colorado.Common.WindowsLibrariesWrappers.Kernel32
{
    public interface IKernel32LibraryWrapper
    {
        IntPtr LoadLibrary(string libraryName);
    }

    public class Kernel32LibraryWrapper : IKernel32LibraryWrapper
    {
        public Kernel32LibraryWrapper() { }

        public IntPtr LoadLibrary(string libraryName)
        {
            return Kernel32LibraryAPI.LoadLibrary(libraryName);
        }
    }
}
