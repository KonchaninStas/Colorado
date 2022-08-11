using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General
{
    internal static class OpenGLAPIGeneral
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnable")]
        public static extern void Enable(int cap);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glDisable")]
        public static extern void Disable(int cap);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glIsEnabled")]
        public static extern bool IsEnabled(int cap);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glCullFace")]
        public static extern bool CullFace(int faceSide);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glFrontFace")]
        public static extern bool SetFrontFaceVerticesOrder(int verticesOrder);
    }
}
