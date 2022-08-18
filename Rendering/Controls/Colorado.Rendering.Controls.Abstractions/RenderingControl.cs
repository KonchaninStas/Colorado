using Colorado.Application;
using Colorado.Common.Colours;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.Geometry3D;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.Abstractions.Utils;
using Colorado.Rendering.Lighting;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public abstract class RenderingControl : IRenderingControl
    {
        protected readonly ITotalBoundingBoxProvider _totalBoundingBoxProvider;
        protected readonly ILightsManager _lightsManager;
        protected readonly IGeometryRenderer _geometryRenderer;

        protected readonly IGridPlane _gridPlane;

        protected RenderingControl(IProgram program, ITotalBoundingBoxProvider totalBoundingBoxProvider,
            ILightsManager lightsManager, IViewport viewport, IGeometryRenderer geometryRenderer)
        {
            Program = program;
            _totalBoundingBoxProvider = totalBoundingBoxProvider;
            _lightsManager = lightsManager;
            Viewport = viewport;
            _geometryRenderer = geometryRenderer;
            BackgroundColor = RGB.BackgroundDefaultColor;

            IBoundingBox boundingBox = program.DocumentsManager.ActiveDocument.Model.TotalBoundingBox;
            _gridPlane = boundingBox.IsEmpty ? new GridPlane() : new GridPlane(5, boundingBox.Diagonal, boundingBox.MinPoint.Z);
            _totalBoundingBoxProvider.AddRenderableObject(_gridPlane);
        }

        public IViewport Viewport { get; }

        public IRGB BackgroundColor { get; }

        public IProgram Program { get; }

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
            _geometryRenderer.DrawCuboid(Program.DocumentsManager.ActiveDocument.Model.TotalBoundingBox.Cuboid, RGB.RedColor);
            _geometryRenderer.DrawCoordinateSystem(100, 2);
            _geometryRenderer.DrawGeometryProvider(_gridPlane.GeometryProvider);
            //_geometryRenderer.DrawRay(Viewport.Camera.RightVector.ToRay(), 200, RGB.RedColor, 10);
            // _geometryRenderer.DrawRay(Viewport.Camera.UpVector.ToRay(), 200, RGB.GreenColor, 10);
        }

        private void DrawSceneGeometry()
        {
            DrawNode(Program.DocumentsManager.ActiveDocument.Model.RootNode);
        }

        private void DrawNode(INode node)
        {
            if (_lightsManager.IsLightingEnabled)
            {
                _geometryRenderer.DrawGeometryProviderWithMaterial(node.Mesh.GeometryProvider, node.GetAbsoluteTransform());
            }
            else
            {
                _geometryRenderer.DrawGeometryProviderWithMaterial(node.Mesh.GeometryProvider, node.GetAbsoluteTransform());
            }

            foreach (INode child in node.Children)
            {
                DrawNode(child);
            }
        }

        public abstract void EndDrawScene();
    }
}
