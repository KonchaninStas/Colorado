using Colorado.Services.Gdi32.Structures;
using System;
using System.Runtime.InteropServices;

namespace Colorado.Services.Gdi32
{
    internal class Gdi32LibraryAPI
    {
        [DllImport("GDI32.dll", SetLastError = true)]
        public static extern int ChoosePixelFormat(IntPtr hDC, [In, MarshalAs(UnmanagedType.LPStruct)] IPixelFormatDescriptor pfd);

        [DllImport("GDI32.dll", SetLastError = true)]
        public static extern bool SetPixelFormat(IntPtr hDC, int format, [In, MarshalAs(UnmanagedType.LPStruct)] IPixelFormatDescriptor pfd);

        [DllImport("GDI32.dll", SetLastError = true)]
        public static extern void SwapBuffers(IntPtr hDC);
    }
}
