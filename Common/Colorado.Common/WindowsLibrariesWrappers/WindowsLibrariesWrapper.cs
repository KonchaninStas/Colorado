using Colorado.Common.WindowsLibrariesWrappers.Gdi32;
using Colorado.Common.WindowsLibrariesWrappers.Kernel32;
using Colorado.Common.WindowsLibrariesWrappers.User32;

namespace Colorado.Common.WindowsLibrariesWrappers
{
    public interface IWindowsLibrariesWrapper
    {
        IGdi32LibraryWrapper Gdi32LibraryWrapper { get; }
        IKernel32LibraryWrapper Kernel32LibraryWrapper { get; }
        IUser32LibraryWrapper User32LibraryWrapper { get; }
    }

    public class WindowsLibrariesWrapper : IWindowsLibrariesWrapper
    {
        public WindowsLibrariesWrapper(IGdi32LibraryWrapper gdi32LibraryWrapper,
            IKernel32LibraryWrapper kernel32LibraryWrapper, IUser32LibraryWrapper user32LibraryWrapper)
        {
            Gdi32LibraryWrapper = gdi32LibraryWrapper;
            Kernel32LibraryWrapper = kernel32LibraryWrapper;
            User32LibraryWrapper = user32LibraryWrapper;
        }

        public IGdi32LibraryWrapper Gdi32LibraryWrapper { get; }

        public IKernel32LibraryWrapper Kernel32LibraryWrapper { get; }

        public IUser32LibraryWrapper User32LibraryWrapper { get; }
    }
}
