using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.WinForms;
using System.Windows.Forms.Integration;

namespace Colorado.Rendering.Controls.WPF.ViewModels
{
    public class WPFRenderingControlViewModel : ViewModelBase
    {
        public WPFRenderingControlViewModel(IRenderingControl renderingControl, IModel model)
        {
            WinFormsRenderingControl = new WindowsFormsHost() { Child = new WinFormsRenderingControl(renderingControl, model) };
        }

        public WindowsFormsHost WinFormsRenderingControl { get; }
    }
}
