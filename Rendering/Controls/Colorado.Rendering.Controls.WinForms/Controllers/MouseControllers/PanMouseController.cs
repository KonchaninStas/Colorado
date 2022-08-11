using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class PanMouseController : Controller
    {
        #region Private fields

        private IPoint2D lastCursorPosition;
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
                IPoint2D newCursorPosition = controllerInputData.MousePositionInfo.CursorPositionInViewportCoordinates;
                controllerInputData.Camera.Pan(lastCursorPosition.Minus(newCursorPosition));
                lastCursorPosition = newCursorPosition;
            }
        }

        #endregion Private logic
    }
}
