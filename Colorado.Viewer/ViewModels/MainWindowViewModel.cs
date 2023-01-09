using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.WPF;
using Colorado.Rendering.Lighting;
using Colorado.Viewer.Controls.ButtonsHelpPanel;
using Colorado.Viewer.Controls.TimeAndLocationSettings;

namespace Colorado.Viewer.ViewModels
{
    public interface IMainWindowViewModel
    {
        int FPS { get; }
        int TrianglesCount { get; }
        WPFRenderingControl WPFRenderingControl { get; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region Private fields

        private readonly IRenderingControl _renderingControl;

        #endregion Private fields

        #region Constructor

        public MainWindowViewModel(IRenderingControl renderingControl, ILightsManager lightsManager)
        {
            _renderingControl = renderingControl;
            _renderingControl.DrawSceneFinished += _renderingControl_DrawSceneFinished;
            WPFRenderingControl = new WPFRenderingControl(_renderingControl);
            ButtonsHelpPanelViewModel = new ButtonsHelpPanelViewModel(_renderingControl.Program.KeyboardCommandsManager);
            TimeAndLocationSettingsViewModel = new TimeAndLocationSettingsViewModel(lightsManager, renderingControl.Viewport.Camera);
        }

        #endregion Constructor

        #region Properties

        public WPFRenderingControl WPFRenderingControl { get; }

        public int FPS
        {
            get
            {
                return (int)_renderingControl.RenderingControlStatistics.FPS;
            }
        }

        public int TrianglesCount
        {
            get
            {
                return _renderingControl.RenderingControlStatistics.TrianglesCount;
            }
        }

        public ButtonsHelpPanelViewModel ButtonsHelpPanelViewModel { get; }

        public ITimeAndLocationSettingsViewModel TimeAndLocationSettingsViewModel { get; }

        #endregion Properties

        #region Private logic

        private void _renderingControl_DrawSceneFinished(object sender, System.EventArgs e)
        {
            OnPropertyChanged(nameof(FPS));
            OnPropertyChanged(nameof(TrianglesCount));
        }

        #endregion Private logic
    }
}
