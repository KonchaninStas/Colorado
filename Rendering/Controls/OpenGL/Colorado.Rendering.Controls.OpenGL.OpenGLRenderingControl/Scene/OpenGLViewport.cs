using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;
using Colorado.Rendering.Controls.Abstractions.Utils;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.View;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene
{
    public class OpenGLViewport : Viewport
    {
        private readonly OpenGLCamera _openGLCamera;

        public OpenGLViewport(OpenGLCamera camera, ITotalBoundingBoxProvider totalBoundingBoxProvider)
            : base(camera, totalBoundingBoxProvider)
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
            Vector2D imageSize = ImageSize;
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

        public override Ray CalculateCursorRay(Point2D cursorPositionInScreenCoordinates)
        {
            Point nearPoint = OpenGLSceneWrapper.ScreenToWorld(cursorPositionInScreenCoordinates, UnprojectPlane.Near);
            Point farPoint = OpenGLSceneWrapper.ScreenToWorld(cursorPositionInScreenCoordinates, UnprojectPlane.Far);

            return new Ray(nearPoint, (farPoint - nearPoint).UnitVector);
        }
    }
}
