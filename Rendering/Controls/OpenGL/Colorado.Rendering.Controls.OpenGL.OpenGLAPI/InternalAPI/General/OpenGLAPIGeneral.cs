using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General
{
    internal static class OpenGLAPIGeneral
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnable")]
        public static extern void Enable(int cap);
    }
}
