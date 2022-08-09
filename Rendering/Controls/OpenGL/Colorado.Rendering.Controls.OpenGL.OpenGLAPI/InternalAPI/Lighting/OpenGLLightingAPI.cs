using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.Lighting
{
    internal static class OpenGLLightingAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightModeli")]
        public static extern void LightModeli(int pname, int param);


        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightModelfv")]
        public static extern void LightModelfv(int pname, float[] fparams);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightf")]
        public static extern void Lightf(int light, int pname, float param);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightfv")]
        public static extern void Lightfv(int light, int pname, float[] fparams);
    }
}
