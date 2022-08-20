using Colorado.Application;
using Colorado.Common.Colours;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.Geometry3D;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.Abstractions.Statistics;
using Colorado.Rendering.Controls.Abstractions.Utils;
using Colorado.Rendering.Lighting;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public abstract class RenderingControl : IRenderingControl
    {
        #region Private fields

        protected readonly ITotalBoundingBoxProvider _totalBoundingBoxProvider;
        protected readonly ILightsManager _lightsManager;
        protected readonly IGeometryRenderer _geometryRenderer;
        protected readonly IGridPlane _gridPlane;

        #endregion Private fields

        #region Constructor

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
            _gridPlane = boundingBox.IsEmpty ? new GridPlane() : new GridPlane((int)boundingBox.Diagonal / 10, boundingBox.Diagonal, boundingBox.MinPoint.Z);
            _totalBoundingBoxProvider.AddRenderableObject(_gridPlane);
            RenderingControlStatistics = new RenderingControlStatistics(
                program.DocumentsManager, new FpsCalculator(this));
        }

        #endregion Constructor

        #region Events

        public event EventHandler DrawSceneStarted;

        public event EventHandler DrawSceneFinished;

        #endregion Events

        #region Properties

        public IViewport Viewport { get; }

        public IRGB BackgroundColor { get; }

        public IProgram Program { get; }

        public IRenderingControlStatistics RenderingControlStatistics { get; }

        #endregion Properties

        #region Public logic

        public abstract void Initialize(IntPtr windowHandle);

        public abstract void Dispose();

        public abstract void BeforeDrawScene();

        public void DrawScene()
        {
            DrawSceneStarted?.Invoke(this, EventArgs.Empty);
            BeforeDrawScene();

            Viewport.Apply();

            _lightsManager.DisableLighting();
            DrawScenePrimitives();

            _lightsManager.ConfigureEnabledLights();
            DrawSceneGeometry();

            EndDrawScene();

            DrawSceneFinished?.Invoke(this, EventArgs.Empty);
        }

        public abstract void EndDrawScene();

        #endregion Public logic

        #region Private logic

        private void DrawScenePrimitives()
        {
            _geometryRenderer.DrawPoint(Viewport.Camera.TargetPoint.Inverse, RGB.RedColor, 10);
            _geometryRenderer.DrawCuboid(_totalBoundingBoxProvider.TotalBoundingBox.Cuboid, RGB.RedColor);
            _geometryRenderer.DrawCuboid(_totalBoundingBoxProvider.NodesBoundingBox.Cuboid, RGB.BlueColor);
            _geometryRenderer.DrawCoordinateSystem(100, 2);
            _geometryRenderer.DrawGeometryProvider(_gridPlane.GeometryProvider);
            DrawNode(Program.DocumentsManager.ActiveDocument.Model.RootNode);
        }

        private void DrawSceneGeometry()
        {
            //DrawNode(Program.DocumentsManager.ActiveDocument.Model.RootNode);
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

        #endregion Private logic
    }
}
