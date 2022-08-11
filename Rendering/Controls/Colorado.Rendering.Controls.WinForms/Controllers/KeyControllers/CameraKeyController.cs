using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal class CameraKeyController : Controller
    {
        public override string Name => nameof(CameraKeyController);

        public override void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData)
        {
            switch (keyCode)
            {
                case Keys.W:
                    MoveCamera(controllerInputData.Camera.UpVector.GetInversed(), controllerInputData.Camera);
                    break;
                case Keys.A:
                    MoveCamera(controllerInputData.Camera.RightVector.GetInversed(), controllerInputData.Camera);
                    break;
                case Keys.S:
                    MoveCamera(controllerInputData.Camera.UpVector, controllerInputData.Camera);
                    break;
                case Keys.D:
                    MoveCamera(controllerInputData.Camera.RightVector, controllerInputData.Camera);
                    break;
                case Keys.Right:
                    controllerInputData.Camera.RotateAroundTarget(controllerInputData.Camera.UpVector, -5);
                    break;
                case Keys.Left:
                    controllerInputData.Camera.RotateAroundTarget(controllerInputData.Camera.UpVector, 5);
                    break;
                case Keys.Up:
                    controllerInputData.Camera.RotateAroundTarget(controllerInputData.Camera.RightVector, -5);
                    break;
                case Keys.Down:
                    controllerInputData.Camera.RotateAroundTarget(controllerInputData.Camera.RightVector, 5);
                    break;
                default:
                    break;
            }
        }

        public override void OnKeyUp(Keys keyCode, IControllerInputData controllerInputData)
        {
            switch (keyCode)
            {
                case Keys.F8:
                    SwitchCameraMode(controllerInputData.Camera);
                    break;
                case Keys.F9:
                    controllerInputData.Viewport.ZoomToFit();
                    break;
                default:
                    break;
            }
        }

        private void MoveCamera(IVector transaltionVector, ICamera camera)
        {
            camera.Translate(transaltionVector);
        }

        private void SwitchCameraMode(ICamera camera)
        {
            camera.CameraType = camera.CameraType == CameraType.Perspective ? CameraType.Orthographic : CameraType.Perspective;
        }
    }
}
