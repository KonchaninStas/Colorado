using Colorado.Common.Utils;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class RotationMouseController : Controller
    {
        #region Private fields

        private IRay lastRay;
        private bool isRotationStarted;

        #endregion Private fields

        public override string Name => nameof(RotationMouseController);

        #region Private logic

        public override void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Right)
            {
                lastRay = controllerInputData.MousePositionInfo.Ray;
                controllerInputData.SetCursorType(Cursors.Cross);
                isRotationStarted = true;
            }
        }

        public override void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData)
        {
            if (button == MouseButtons.Right && isRotationStarted)
            {
                var plane = new Plane(controllerInputData.Camera.TargetPoint, controllerInputData.Camera.DirectionVector);
                IVector firstVector = new Vector(controllerInputData.Camera.TargetPoint, plane.GetIntersectionPoint(lastRay)).UnitVector;
                IVector secondVector = new Vector(controllerInputData.Camera.TargetPoint, plane.GetIntersectionPoint(controllerInputData.MousePositionInfo.Ray)).UnitVector;

                controllerInputData.Camera.RotateAroundTarget(firstVector.CrossProduct(secondVector),
                    MathUtils.Instance.ConvertRadiansToDegrees(firstVector.AngleToVectorInRadians(secondVector)));
                lastRay = controllerInputData.MousePositionInfo.Ray;
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
