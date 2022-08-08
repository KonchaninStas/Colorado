using System.Runtime.InteropServices;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.View
{
    internal static class OpenGLSceneAPI
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="red">0-1</param>
        /// <param name="green">0-1</param>
        /// <param name="blue">0-1</param>
        /// <param name="alpha">0-1</param>
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClearColor")]
        public static extern void ClearColor(float red, float green, float blue, float alpha);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glShadeModel")]
        public static extern void ShadeModel(int mode);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClearDepth")]
        public static extern void ClearDepth(double depth);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClear")]
        public static extern void Clear(int mask);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glFlush")]
        public static extern void Flush();
    }
}
