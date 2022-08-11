using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal class DefaultViewSwitchingController : Controller
    {
        public override string Name => nameof(DefaultViewSwitchingController);

        public override void OnKeyUp(Keys keyCode, IControllerInputData controllerInputData)
        {
            switch (keyCode)
            {
                case Keys.F1:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Front);
                    break;
                case Keys.F2:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Top);
                    break;
                case Keys.F3:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Rear);
                    break;
                case Keys.F4:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Right);
                    break;
                case Keys.F5:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Left);
                    break;
                case Keys.F6:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Bottom);
                    break;
                case Keys.F7:
                    controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Iso);
                    break;
                default:
                    break;
            }
        }
    }
}
