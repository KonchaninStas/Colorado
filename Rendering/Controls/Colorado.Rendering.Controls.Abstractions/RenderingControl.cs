using Colorado.Common.Colours;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Lighting;
using Colorado.Rendering.Materials;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public abstract class RenderingControl : IRenderingControl
    {
        protected readonly ILightsManager _lightsManager;
        protected readonly IGeometryRenderer _geometryRenderer;

        protected RenderingControl(IModel model, ILightsManager lightsManager, IViewport viewport,
            IGeometryRenderer geometryRenderer)
        {
            Model = model;
            _lightsManager = lightsManager;
            Viewport = viewport;
            _geometryRenderer = geometryRenderer;
            BackgroundColor = RGB.BackgroundDefaultColor;
        }

        public IViewport Viewport { get; }

        public IRGB BackgroundColor { get; }

        public IModel Model { get; }

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
            _geometryRenderer.DrawPoint(Viewport.Camera.TargetPoint.Inverse, RGB.RedColor, 10);
            _geometryRenderer.DrawCuboid(Model.TotalBoundingBox.Cuboid, RGB.RedColor);
            _geometryRenderer.DrawCoordinateSystem(100);
            //DrawNode(Model.RootNode, false);
        }

        private void DrawSceneGeometry()
        {
            DrawNode(Model.RootNode, true);
        }

        private void DrawNode(INode node, bool useMaterial)
        {
            if (useMaterial)
            {
                _geometryRenderer.DrawMeshWithMaterial(node.Mesh, node.GetAbsoluteTransform());
            }
            else
            {
                _geometryRenderer.DrawMesh(node.Mesh, node.GetAbsoluteTransform());
            }

            foreach (INode child in node.Children)
            {
                DrawNode(child, useMaterial);
            }
        }

        public abstract void EndDrawScene();
    }
}
