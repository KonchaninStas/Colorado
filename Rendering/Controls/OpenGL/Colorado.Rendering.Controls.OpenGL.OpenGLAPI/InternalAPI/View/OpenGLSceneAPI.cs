﻿using System.Runtime.InteropServices;

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

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glViewport")]
        public static extern void Viewport(int x, int y, int width, int height);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glOrtho")]
        public static extern void Ortho(double left, double right, double bottom, double top, double zNear, double zFar);

        [DllImport(OpenGLLibraryNames.OpenGLU32LibraryName, EntryPoint = "gluPerspective")]
        public static extern void Perspective(double fovy, double aspect, double zNear, double zFar);

        [DllImport(OpenGLLibraryNames.OpenGLU32LibraryName, EntryPoint = "gluUnProject")]
        public static extern void gluUnProject(double winx, double winy, double winz,
         double[] modelMatrix, double[] projMatrix, int[] viewport, ref double objx,
         ref double objy, ref double objz);
    }
}
