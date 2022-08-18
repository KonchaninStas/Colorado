using Colorado.Rendering.Controls.WinForms.Controllers.Data;

namespace Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers
{
    internal class ZoomMouseController : Controller
    {
        #region Properties

        public override string Name => nameof(ZoomMouseController);

        #endregion Properties

        #region Public logic

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

        #endregion Public logic
    }
}
