using Colorado.Help.Keyboard;
using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;
using System.Windows.Forms;

using Strings = Colorado.Resources.Properties.Resources;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal class DefaultViewSwitchingController : BaseKeyController
    {
        internal DefaultViewSwitchingController()
        {
            AddCommand(new KeyboardCommand(Keys.F1, Strings.KeyCommand_SetFrontCameraView_Name, Strings.KeyCommand_SetFrontCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Front));
            AddCommand(new KeyboardCommand(Keys.F2, Strings.KeyCommand_SetRearCameraView_Name, Strings.KeyCommand_SetRearCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Rear));
            AddCommand(new KeyboardCommand(Keys.F3, Strings.KeyCommand_SetTopCameraView_Name, Strings.KeyCommand_SetTopCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Top));
            AddCommand(new KeyboardCommand(Keys.F4, Strings.KeyCommand_SetBottomCameraView_Name, Strings.KeyCommand_SetBottomCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Bottom));
            AddCommand(new KeyboardCommand(Keys.F5, Strings.KeyCommand_SetRightCameraView_Name, Strings.KeyCommand_SetRightCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Right));
            AddCommand(new KeyboardCommand(Keys.F6, Strings.KeyCommand_SetLeftCameraView_Name, Strings.KeyCommand_SetLeftCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Left));
            AddCommand(new KeyboardCommand(Keys.F7, Strings.KeyCommand_SetIsoCameraView_Name, Strings.KeyCommand_SetIsoCameraView_Description),
                (controllerInputData) => controllerInputData.Camera.DefaultViewsManager.SetDefaultCameraView(DefaultCameraView.Iso));
        }

        #region Properties

        public override string Name => nameof(DefaultViewSwitchingController);

        #endregion Properties
    }
}
