using Colorado.Common.Colours;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Lighting;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public abstract class RenderingControl : IRenderingControl
    {
        private readonly IModel _model;
        protected readonly ILightsManager _lightsManager;
        protected readonly IGeometryRenderer _geometryRenderer;

        protected RenderingControl(IModel model, ILightsManager lightsManager, IViewport viewport, IGeometryRenderer geometryRenderer)
        {
            _model = model;
            _lightsManager = lightsManager;
            Viewport = viewport;
            _geometryRenderer = geometryRenderer;
            BackgroundColor = RGB.RedColor;
        }

        public IViewport Viewport { get; }

        public RGB BackgroundColor { get; }

        public abstract void Initialize(IntPtr windowHandle);

        public abstract void Dispose();

        public abstract void BeforeDrawScene();

        public void DrawScene()
        {
            BeforeDrawScene();

            Viewport.Apply();

            _lightsManager.DisableLighting();
            DrawScenePrimitives();

            _lightsManager.ConfigureEnabledLights();
            DrawSceneGeometry();

            EndDrawScene();
        }

        private void DrawScenePrimitives()
        {

        }

        private void DrawSceneGeometry()
        {
            DrawNode(_model.RootNode);
        }

        private void DrawNode(INode node)
        {
            _geometryRenderer.DrawTriangles(node.Mesh.Triangles);

            foreach (INode child in node.Children)
            {
                DrawNode(child);
            }
        }

        public abstract void EndDrawScene();
    }
}
