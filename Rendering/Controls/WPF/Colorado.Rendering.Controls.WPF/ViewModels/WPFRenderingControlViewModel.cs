using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Rendering.Controls.WinForms;

namespace Colorado.Rendering.Controls.WPF.ViewModels
{
    public interface IWPFRenderingControlViewModel
    {
        WinFormsRenderingControl WinFormsRenderingControl { get; }
    }

    public class WPFRenderingControlViewModel : ViewModelBase, IWPFRenderingControlViewModel
    {
        public WPFRenderingControlViewModel()
        {
            WinFormsRenderingControl = new WinFormsRenderingControl();
        }

        public WinFormsRenderingControl WinFormsRenderingControl { get; }
    }
}
