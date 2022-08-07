﻿using Colorado.OpenGLAPI.InternalAPI.General;
using System;

namespace Colorado.OpenGLAPI.Wrappers.General
{
    public static class OpenGlWglWrapper
    {
        public static IntPtr CreateContext(IntPtr deviceContextHandle)
        {
            return OpenGlWglAPI.CreateContext(deviceContextHandle);
        }

        public static int SetCurrentRenderingContext(IntPtr deviceContextHandle, IntPtr renderingContextHandle)
        {
            return OpenGlWglAPI.MakeCurrent(deviceContextHandle, renderingContextHandle);
        }

        public static int DeleteOpenGLRenderingContext(IntPtr renderingContextHandle)
        {
            return OpenGlWglAPI.DeleteContext(renderingContextHandle);
        }

        public static IntPtr CreateOpenGLRenderingContext(IntPtr deviceContextHandle)
        {
            return OpenGlWglAPI.CreateContext(deviceContextHandle);
        }

        public static int GetExtensionFunctionAddress(string functionName)
        {
            return OpenGlWglAPI.GetProcAddress(functionName);
        }
    }
}
