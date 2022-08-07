using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl;
using Colorado.Rendering.Controls.WPF;

namespace Colorado.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            WPFRenderingControl = new WPFRenderingControl(new OpenGLRenderingControl());
        }

        public WPFRenderingControl WPFRenderingControl { get; }
    }
}
