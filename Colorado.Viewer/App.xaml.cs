using Colorado.Services;
using System.Windows;

namespace Colorado.Viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Host.Instance.Dispose();
        }
    }
}
