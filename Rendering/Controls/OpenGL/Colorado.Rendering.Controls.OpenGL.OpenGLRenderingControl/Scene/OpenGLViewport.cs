using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.View;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene
{
    public class OpenGLViewport : Viewport
    {
        private readonly OpenGLCamera _openGLCamera;

        public OpenGLViewport(OpenGLCamera camera, IModel model) : base(camera, model)
        {
            _openGLCamera = camera;
        }

        public override void Apply()
        {
            OpenGLSceneWrapper.SetViewport(0, 0, Width, Height);

            _openGLCamera.ApplyProjectionMatrix();

            ApplySettings();

            _openGLCamera.ApplyModelViewMatrix();
        }

        private void ApplySettings()
        {
            if (Camera.CameraType == CameraType.Orthographic)
            {
                ApplyOrthographicCameraSettings();
            }
            else
            {
                ApplyPerspectiveCameraSettings();
            }
        }

        private void ApplyOrthographicCameraSettings()
        {
            IVector2D imageSize = ImageSize;
            double xmin = -imageSize.X / 2;
            double xmax = imageSize.X / 2;
            double ymin = -imageSize.Y / 2;
            double ymax = imageSize.Y / 2;
            OpenGLSceneWrapper.SetOrthographicViewSettings(xmin, xmax, ymin, ymax, NearClip, FarClip);
        }

        private void ApplyPerspectiveCameraSettings()
        {
            OpenGLSceneWrapper.SetPerspectiveCameraSettings(VerticalFieldOfViewInDegrees, AspectRatio, NearClip, FarClip);
        }

        public override IRay CalculateCursorRay(IPoint2D cursorPositionInScreenCoordinates)
        {
            IPoint nearPoint = OpenGLSceneWrapper.ScreenToWorld(cursorPositionInScreenCoordinates, UnprojectPlane.Near);
            IPoint farPoint = OpenGLSceneWrapper.ScreenToWorld(cursorPositionInScreenCoordinates, UnprojectPlane.Far);

            return new Ray(nearPoint, farPoint.Minus(nearPoint).UnitVector);
        }
    }
}
