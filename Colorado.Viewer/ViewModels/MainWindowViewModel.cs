using Colorado.Application;
using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.Abstractions.Utils;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Managers;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene;
using Colorado.Rendering.Controls.WPF;
using System.Linq;

namespace Colorado.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private fields

        private readonly IRenderingControl _renderingControl;

        #endregion Private fields

        #region Constructor

        public MainWindowViewModel()
        {
            IProgram program = new Program();

            program.DocumentsManager.OpenDocument(
                program.DocumentsManager.GetDefaultDocumentsNames().FirstOrDefault(d => d.Contains("Star")));

            ITotalBoundingBoxProvider totalBoundingBoxProvider = new TotalBoundingBoxProvider(program.DocumentsManager);
            _renderingControl = new OpenGLRenderingControl(program,
                totalBoundingBoxProvider, new OpenGLLightsManager(),
                new OpenGLViewport(new OpenGLCamera(totalBoundingBoxProvider), totalBoundingBoxProvider),
                new OpenGLGeometryRenderer(new OpenGLMaterialsManager()));

            _renderingControl.DrawSceneFinished += _renderingControl_DrawSceneFinished;
            WPFRenderingControl = new WPFRenderingControl(_renderingControl);
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
