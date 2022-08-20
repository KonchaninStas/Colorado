using System;
using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.Rendering
{
    internal static class OpenGLRenderingAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glColor4d")]
        public static extern void SetColor(double red, double green, double blue, double intensity);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glVertex3d")]
        public static extern void SetVertex(double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glPolygonMode")]
        public static extern void PolygonMode(int face, int mode);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnableClientState")]
        public static extern void EnableClientState(int array);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glDisableClientState")]
        public static extern void DisableClientState(int array);


        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glVertexPointer")]
        public static extern void VertexPointer(int size, int type, int stride, IntPtr pointer);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glNormalPointer")]
        public static extern void NormalPointer(int type, int stride, IntPtr pointer);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glColorPointer")]
        public static extern void ColorPointer(int size, int type, int stride, IntPtr pointer);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glDrawArrays")]
        public static extern void glDrawArrays(int primitive, int startIndex, int numberOfIndexes);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glPointSize")]
        public static extern void PointSize(float size);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glBegin")]
        public static extern void Begin(int primitive);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnd")]
        public static extern void End();

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLineWidth")]
        public static extern void SetLineWidth(float width);
    }
}
