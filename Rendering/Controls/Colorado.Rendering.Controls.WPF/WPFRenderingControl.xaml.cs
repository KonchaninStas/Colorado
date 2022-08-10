using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.WPF.ViewModels;
using System.Windows.Controls;

namespace Colorado.Rendering.Controls.WPF
{
    /// <summary>
    /// Interaction logic for WPFRenderingControl.xaml
    /// </summary>
    public partial class WPFRenderingControl : UserControl
    {
        public WPFRenderingControl(IRenderingControl renderingControl)
        {
            InitializeComponent();
            DataContext = new WPFRenderingControlViewModel(renderingControl);
        }
    }
}
