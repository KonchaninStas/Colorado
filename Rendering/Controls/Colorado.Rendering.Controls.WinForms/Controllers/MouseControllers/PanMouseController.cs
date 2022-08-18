using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class PanMouseController : Controller
    {
        #region Private fields

        private Point2D _lastCursorPosition;
        private bool _isPanStarted;

        #endregion Private fields

        #region Properties

        public override string Name => nameof(PanMouseController);

        #endregion Properties

        #region Private logic

        public override void OnMouseUp(MouseButtons button, IControllerInputData controllerInputData)
        {
            _isPanStarted = false;
            controllerInputData.SetCursorType(Cursors.Default);
        }

        public override void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Middle)
            {
                _lastCursorPosition = controllerInputData.MousePositionInfo.CursorPositionInViewportCoordinates;
                _isPanStarted = true;
                controllerInputData.SetCursorType(Cursors.Hand);
            }
        }

        public override void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (_isPanStarted)
            {
                Point2D newCursorPosition = controllerInputData.MousePositionInfo.CursorPositionInViewportCoordinates;
                controllerInputData.Camera.Pan(_lastCursorPosition - newCursorPosition);
                _lastCursorPosition = newCursorPosition;
            }
        }

        #endregion Private logic
    }
}
