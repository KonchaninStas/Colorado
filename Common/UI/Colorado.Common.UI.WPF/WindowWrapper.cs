using Colorado.Common.UI.WPF.ViewModels.Base;
using System.Windows;

namespace Colorado.Common.UI.WPF
{
    public interface IWindowWrapper
    {
        void Show();
        void ShowDialog();
    }

    public class WindowWrapper : IWindowWrapper
    {
        private readonly Window _window;

        public WindowWrapper(Window window, IViewModelBase viewModelBase)
        {
            _window = window;
            _window.DataContext = viewModelBase;
        }

        public void Show()
        {
            _window.Show();
        }

        public void ShowDialog()
        {
            _window.ShowDialog();
        }
    }
}
