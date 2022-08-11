using Colorado.Rendering.Controls.WinForms.Controllers.Data;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class ZoomMouseController : Controller
    {
        public override string Name => nameof(ZoomMouseController);

        #region Private logic

        public override void OnMouseWheel(int delta, IControllerInputData controllerInputData)
        {
            if (delta > 0)
            {
                controllerInputData.Camera.Zoom(0.5);
            }
            else
            {
                controllerInputData.Camera.Zoom(1.5);
            }
        }

        #endregion Private logic
    }
}
