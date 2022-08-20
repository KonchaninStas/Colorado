using Colorado.Application;
using Colorado.Common.Logging;
using Colorado.Common.UI.WPF;
using Colorado.Common.UI.WPF.Services;
using Colorado.Common.WindowsLibrariesWrappers;
using Colorado.Common.WindowsLibrariesWrappers.Gdi32;
using Colorado.Common.WindowsLibrariesWrappers.Kernel32;
using Colorado.Common.WindowsLibrariesWrappers.User32;
using Colorado.Documents;
using Colorado.Documents.Readers.STLDocumentReader;
using Colorado.Rendering.Controls.Abstractions.Utils;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Managers;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene;
using Colorado.Viewer.ViewModels;
using System.Linq;

namespace Colorado.Viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            var documentsManager = new DocumentsManager();
            documentsManager.RegisterFileReader(new STLDocumentReader());

            var windowsLibrariesWrapper = new WindowsLibrariesWrapper(new Gdi32LibraryWrapper(),
                new Kernel32LibraryWrapper(), new User32LibraryWrapper());

            var program = new Program(documentsManager, new Logger(), windowsLibrariesWrapper,
                new WPFMessageBoxService());

            program.DocumentsManager.OpenDocument(
                program.DocumentsManager.GetDefaultDocumentsNames().FirstOrDefault(d => d.Contains("Star")));

            ITotalBoundingBoxProvider totalBoundingBoxProvider = new TotalBoundingBoxProvider(program.DocumentsManager);
            var renderingControl = new OpenGLRenderingControl(program,
                totalBoundingBoxProvider, new OpenGLLightsManager(),
                    new OpenGLViewport(new OpenGLCamera(totalBoundingBoxProvider), totalBoundingBoxProvider),
                    new OpenGLGeometryRenderer(new OpenGLMaterialsManager()));

            IWindowWrapper windowWrapper = new WindowWrapper(new MainWindow(), new MainWindowViewModel(renderingControl));
            windowWrapper.Show();
        }
    }
}
