using Colorado.Common.Colours;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.View;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.View
{
    public static class OpenGLSceneWrapper
    {
        public static void ClearColor(RGB colorToClear)
        {
            float[] valuesInFloat = colorToClear.ToFloat4Array();
            OpenGLSceneAPI.ClearColor(valuesInFloat[0], valuesInFloat[1], valuesInFloat[2], valuesInFloat[3]);
        }

        public static void SetShadingMode(ShadingModel shadingModel)
        {
            OpenGLSceneAPI.ShadeModel((int)shadingModel);
        }

        public static void ClearDepthBufferValue()
        {
            OpenGLSceneAPI.ClearDepth(1);
        }

        public static void ClearBuffers(params OpenGLBufferType[] bufferTypes)
        {
            foreach (OpenGLBufferType bufferType in bufferTypes)
            {
                OpenGLSceneAPI.Clear((int)bufferType);
            }
        }

        public static void Flush()
        {
            OpenGLSceneAPI.Flush();
        }
    }
}
