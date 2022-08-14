using System;
using System.Runtime.InteropServices;

namespace Colorado.Common.WindowsLibrariesWrappers.Kernel32
{
    internal class Kernel32LibraryAPI
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(String funcname);
    }
}
