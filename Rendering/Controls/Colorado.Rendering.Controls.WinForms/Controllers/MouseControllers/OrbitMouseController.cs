using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class OrbitMouseController : Controller
    {
        #region Private fields

        private Point2D _lastPoint;
        private bool _isRotationStarted;

        #endregion Private fields

        public override string Name => nameof(OrbitMouseController);

        #region Private logic

        public override void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Left)
            {
                _lastPoint = controllerInputData.MousePositionInfo.CursorPositionInScreenCoordinates;
                controllerInputData.SetCursorType(Cursors.Cross);
                _isRotationStarted = true;
            }
        }

        public override void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Left && _isRotationStarted)
            {
                controllerInputData.Camera.RotateAroundTarget(_lastPoint, controllerInputData.MousePositionInfo.CursorPositionInScreenCoordinates);
                _lastPoint = controllerInputData.MousePositionInfo.CursorPositionInScreenCoordinates;
            }
        }

        public override void OnMouseUp(MouseButtons button, IControllerInputData controllerInputData)
        {
            base.OnMouseUp(button, controllerInputData);
            _isRotationStarted = false;
            controllerInputData.SetCursorType(Cursors.Default);
        }

        #endregion Private logic
    }
}
