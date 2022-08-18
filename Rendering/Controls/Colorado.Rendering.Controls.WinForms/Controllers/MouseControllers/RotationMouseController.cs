using Colorado.Common.Utils;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class RotationMouseController : Controller
    {
        #region Private fields

        private Ray _lastRay;
        private bool _isRotationStarted;

        #endregion Private fields

        public override string Name => nameof(RotationMouseController);

        #region Private logic

        public override void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Right)
            {
                _lastRay = controllerInputData.MousePositionInfo.Ray;
                controllerInputData.SetCursorType(Cursors.Cross);
                _isRotationStarted = true;
            }
        }

        public override void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Right && _isRotationStarted)
            {
                Plane plane = controllerInputData.Camera.TargetPointPlane;
                Vector firstVector = new Vector(controllerInputData.Camera.TargetPoint, plane.GetIntersectionPoint(_lastRay)).UnitVector;
                Vector secondVector = new Vector(controllerInputData.Camera.TargetPoint, plane.GetIntersectionPoint(controllerInputData.MousePositionInfo.Ray)).UnitVector;

                controllerInputData.Camera.RotateAroundTarget(firstVector.CrossProduct(secondVector),
                    MathUtils.Instance.ConvertRadiansToDegrees(firstVector.AngleToVectorInRadians(secondVector)));
                _lastRay = controllerInputData.MousePositionInfo.Ray;
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
