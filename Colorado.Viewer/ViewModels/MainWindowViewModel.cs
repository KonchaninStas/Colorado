using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.MeshStructure;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Lighting;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene;
using Colorado.Rendering.Controls.WPF;
using System.Collections.Generic;

namespace Colorado.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var model = new Model(new Node(new Mesh(new List<ITriangle>())));
            WPFRenderingControl = new WPFRenderingControl(
                new OpenGLRenderingControl(new OpenGLLightsManager(), new OpenGLViewport(new OpenGLCamera(), model)), model);
        }

        public WPFRenderingControl WPFRenderingControl { get; }
    }
}
