using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.View;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Structures;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.View
{
    public static class OpenGLSceneWrapper
    {
        public static void ClearColor(IRGB colorToClear)
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

        public static void SetViewport(int x, int y, int width, int height)
        {
            OpenGLSceneAPI.Viewport(x, y, width, height);
        }

        public static void SetOrthographicViewSettings(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            OpenGLSceneAPI.Ortho(left, right, bottom, top, zNear, zFar);
        }

        public static void SetPerspectiveCameraSettings(double verticalFieldOfViewInDegrees, double aspectRatio,
            double distanceToNearPlane, double distanceToFarPlane)
        {
            OpenGLSceneAPI.Perspective(verticalFieldOfViewInDegrees, aspectRatio, distanceToNearPlane, distanceToFarPlane);
        }

        private static Viewport GetViewport()
        {
            int[] viewportValues = OpenGLMatrixOperationWrapper.GetParameterValuesArray(OpenGLCapability.Viewport, 4);
            return new Viewport(viewportValues[0], viewportValues[1],
                viewportValues[2], viewportValues[3]);
        }

        public static IPoint ScreenToWorld(IPoint2D point, UnprojectPlane unprojectPlane)
        {
            double realY = GetViewport().Height - point.Y - 1;
            double wX = 0.0;
            double wY = 0.0;
            double wZ = 0.0;
            OpenGLSceneAPI.gluUnProject(point.X, realY, (int)unprojectPlane, OpenGLMatrixOperationWrapper.GetModelViewMatrix().Array,
                OpenGLMatrixOperationWrapper.GetProjectionMatrix().Array,
                GetViewport().Array, ref wX, ref wY, ref wZ);

            return new Point(wX, wY, wZ);
        }
    }
}
