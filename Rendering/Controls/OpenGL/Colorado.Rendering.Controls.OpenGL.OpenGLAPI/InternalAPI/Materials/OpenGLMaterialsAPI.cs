using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.Materials
{
    internal static class OpenGLMaterialsAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMaterialf")]
        public static extern void Materialf(int face, int pname, float param);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMaterialfv")]
        public static extern void Materialfv(int face, int pname, float[] fparams);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glBlendFunc")]
        public static extern void SetBlendFunction(int sourceBlendingFactors, int destinationBlendingFactors);
    }
}
