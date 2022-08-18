using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal sealed class CameraKeyController : Controller
    {
        #region Properties

        public override string Name => nameof(CameraKeyController);

        #endregion Properties

        #region Public logic

        public override void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData)
        {
            switch (keyCode)
            {
                case Keys.W:
                    Scale(controllerInputData, 0.9);
                    break;
                case Keys.S:
                    Scale(controllerInputData, 1.1);
                    break;
                case Keys.A:
                    MoveCamera(controllerInputData.Camera.RightVector.Inversed, controllerInputData.Camera);
                    break;
                case Keys.D:
                    MoveCamera(controllerInputData.Camera.RightVector, controllerInputData.Camera);
                    break;
                case Keys.ShiftKey:
                    MoveCamera(controllerInputData.Camera.UpVector.Inversed, controllerInputData.Camera);
                    break;
                case Keys.ControlKey:
                    MoveCamera(controllerInputData.Camera.UpVector, controllerInputData.Camera);
                    break;
                case Keys.Right:
                    controllerInputData.Camera.RotateAroundTarget(Vector.ZAxis, -5);
                    break;
                case Keys.Left:
                    controllerInputData.Camera.RotateAroundTarget(Vector.ZAxis, 5);
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

        #endregion Public logic

        #region Private logic

        private static void Scale(IControllerInputData controllerInputData, double scaleFactorSing)
        {
            controllerInputData.Camera.SetDistanceToTarget(controllerInputData.Camera.FocalLength * scaleFactorSing);
        }

        private void MoveCamera(Vector transaltionVector, ICamera camera)
        {
            camera.Translate(transaltionVector);
        }

        private void SwitchCameraMode(ICamera camera)
        {
            camera.CameraType = camera.CameraType == CameraType.Perspective ? CameraType.Orthographic : CameraType.Perspective;
        }

        #endregion Private logic
    }
}
