using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class PanMouseController : Controller
    {
        #region Private fields

        private Point2D lastCursorPosition;
        private bool isPanStarted;

        #endregion Private fields

        #region Properties

        public override string Name => nameof(PanMouseController);

        #endregion Properties

        #region Private logic

        public override void OnMouseUp(MouseButtons button, IControllerInputData controllerInputData)
        {
            isPanStarted = false;
            controllerInputData.SetCursorType(Cursors.Default);
        }

        public override void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Middle)
            {
                lastCursorPosition = controllerInputData.MousePositionInfo.CursorPositionInViewportCoordinates;
                isPanStarted = true;
                controllerInputData.SetCursorType(Cursors.Hand);
            }
        }

        public override void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (isPanStarted)
            {
                Point2D newCursorPosition = controllerInputData.MousePositionInfo.CursorPositionInViewportCoordinates;
                controllerInputData.Camera.Pan(lastCursorPosition - newCursorPosition);
                lastCursorPosition = newCursorPosition;
            }
        }

        #endregion Private logic
    }
}
