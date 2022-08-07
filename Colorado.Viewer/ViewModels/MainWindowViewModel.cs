using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Rendering.Controls.WPF;

namespace Colorado.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            WPFRenderingControl = new WPFRenderingControl();
        }

        public WPFRenderingControl WPFRenderingControl { get; }
    }
}
