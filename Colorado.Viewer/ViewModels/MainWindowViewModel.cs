using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.FileReaders.STLFileReaders;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Lighting;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene;
using Colorado.Rendering.Controls.WPF;

namespace Colorado.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            //var triangles = new List<ITriangle>();

            //for (int i = 0; i < 10; i++)
            //{
            //    triangles.Add(Triangle.GetRandomTriangle());
            //}
            var model = new Model(new Node(STLFileReader.Read(@"D:\Projects\Colorado Obsolete\Colorado\Viewer\Content\STLModels\John_Deere.stl")));
            IRenderingControl renderingControl = new OpenGLRenderingControl(model,
                new OpenGLLightsManager(),
                new OpenGLViewport(new OpenGLCamera(), model),
                new OpenGLGeometryRenderer());

            WPFRenderingControl = new WPFRenderingControl(renderingControl);
        }

        public WPFRenderingControl WPFRenderingControl { get; }
    }
}
