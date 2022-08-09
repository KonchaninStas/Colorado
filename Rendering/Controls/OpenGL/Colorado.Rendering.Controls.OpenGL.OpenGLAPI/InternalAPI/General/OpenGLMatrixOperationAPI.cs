using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General
{
    internal static class OpenGLMatrixOperationAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMatrixMode")]
        public static extern void MatrixMode(int mode);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLoadIdentity")]
        public static extern void LoadIdentity();

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glTranslated")]
        public static extern void Translated(double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLoadMatrixd")]
        public static extern void LoadMatrixd(double[] m);
    }
}
