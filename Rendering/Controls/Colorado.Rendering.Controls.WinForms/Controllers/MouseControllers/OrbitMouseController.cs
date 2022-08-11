using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using Colorado.Services.Math;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class OrbitMouseController : Controller
    {
        #region Private fields

        private IPoint2D lastPoint;
        private bool isRotationStarted;

        #endregion Private fields

        public override string Name => nameof(OrbitMouseController);

        #region Private logic

        public override void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Left)
            {
                lastPoint = controllerInputData.MousePositionInfo.CursorPositionInScreenCoordinates;
                controllerInputData.SetCursorType(Cursors.Cross);
                isRotationStarted = true;
            }
        }

        public override void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Left && isRotationStarted)
            {
                controllerInputData.Camera.RotateAroundTarget(lastPoint, controllerInputData.MousePositionInfo.CursorPositionInScreenCoordinates);
                lastPoint = controllerInputData.MousePositionInfo.CursorPositionInScreenCoordinates;
            }
        }

        public override void OnMouseUp(MouseButtons button, IControllerInputData controllerInputData)
        {
            base.OnMouseUp(button, controllerInputData);
            isRotationStarted = false;
            controllerInputData.SetCursorType(Cursors.Default);
        }

        #endregion Private logic
    }
}
