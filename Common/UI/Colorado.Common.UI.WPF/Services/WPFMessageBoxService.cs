using Colorado.Common.Services;
using System;
using System.Windows;

namespace Colorado.Common.UI.WPF.Services
{
    public class WPFMessageBoxService : MessageBoxService
    {
        public override void ShowInformationMessage(string title, string message)
        {
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public override void ShowExceptionMessage(string title, string message, Exception ex)
        {
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public override void ShowExceptionMessage(string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}
