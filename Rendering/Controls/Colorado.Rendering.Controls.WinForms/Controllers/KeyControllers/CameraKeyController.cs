using Colorado.Geometry.Structures.Primitives;
using Colorado.Help.Keyboard;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

using Strings = Colorado.Resources.Properties.Resources;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal sealed class CameraKeyController : BaseKeyController
    {
        internal CameraKeyController()
        {
            AddCommand(new KeyboardCommand(Keys.W, Strings.KeyCommand_ScaleIn_Name, Strings.KeyCommand_ScaleIn_Description),
                (controllerInputData) => MoveCamera(controllerInputData.Camera.DirectionVector, controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.S, Strings.KeyCommand_ScaleOut_Name, Strings.KeyCommand_ScaleOut_Description),
                (controllerInputData) => MoveCamera(controllerInputData.Camera.DirectionVector.Inversed, controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.A, Strings.KeyCommand_MoveCameraLeft_Name, Strings.KeyCommand_MoveCameraLeft_Description),
                (controllerInputData) => MoveCamera(controllerInputData.Camera.RightVector.Inversed, controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.D, Strings.KeyCommand_MoveCameraRight_Name, Strings.KeyCommand_MoveCameraRight_Description),
                (controllerInputData) => MoveCamera(controllerInputData.Camera.RightVector, controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.ShiftKey, Strings.KeyCommand_MoveCameraUp_Name, Strings.KeyCommand_MoveCameraUp_Description),
                (controllerInputData) => MoveCamera(controllerInputData.Camera.UpVector.Inversed, controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.ControlKey, Strings.KeyCommand_MoveCameraDown_Name, Strings.KeyCommand_MoveCameraDown_Description),
                (controllerInputData) => MoveCamera(controllerInputData.Camera.UpVector, controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.Right, Strings.KeyCommand_RotateAroundTargetRight_Name, Strings.KeyCommand_RotateAroundTargetRight_Description),
                (controllerInputData) => controllerInputData.Camera.RotateAroundTarget(Vector.ZAxis, -5));
            AddCommand(new KeyboardCommand(Keys.Left, Strings.KeyCommand_RotateAroundTargetLeft_Name, Strings.KeyCommand_RotateAroundTargetLeft_Description),
                (controllerInputData) => controllerInputData.Camera.RotateAroundTarget(Vector.ZAxis, 5));
            AddCommand(new KeyboardCommand(Keys.Up, Strings.KeyCommand_RotateAroundTargetUp_Name, Strings.KeyCommand_RotateAroundTargetUp_Description),
                (controllerInputData) => controllerInputData.Camera.RotateAroundTarget(controllerInputData.Camera.RightVector, -5));
            AddCommand(new KeyboardCommand(Keys.Down, Strings.KeyCommand_RotateAroundTargetDown_Name, Strings.KeyCommand_RotateAroundTargetDown_Description),
                (controllerInputData) => controllerInputData.Camera.RotateAroundTarget(controllerInputData.Camera.RightVector, 5));

            AddCommand(new KeyboardCommand(Keys.F8, Strings.KeyCommand_SwitchCameraMode_Name, Strings.KeyCommand_SwitchCameraMode_Description),
                (controllerInputData) => SwitchCameraMode(controllerInputData.Camera));
            AddCommand(new KeyboardCommand(Keys.F9, Strings.KeyCommand_ZoomToFit_Name, Strings.KeyCommand_ZoomToFit_Description),
                (controllerInputData) => controllerInputData.Viewport.ZoomToFit());
        }

        #region Properties

        public override string Name => nameof(CameraKeyController);

        #endregion Properties

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
