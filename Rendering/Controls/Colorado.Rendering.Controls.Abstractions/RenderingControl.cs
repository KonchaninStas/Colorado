﻿using Colorado.Application;
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
        protected readonly ILightsManager _lightsManager;
        protected readonly IGeometryRenderer _geometryRenderer;

        protected RenderingControl(IProgram program, ILightsManager lightsManager, IViewport viewport,
            IGeometryRenderer geometryRenderer)
        {
            Program = program;
            _lightsManager = lightsManager;
            Viewport = viewport;
            _geometryRenderer = geometryRenderer;
            BackgroundColor = RGB.BackgroundDefaultColor;
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
            //_geometryRenderer.DrawRay(Viewport.Camera.RightVector.ToRay(), 200, RGB.RedColor, 10);
           // _geometryRenderer.DrawRay(Viewport.Camera.UpVector.ToRay(), 200, RGB.GreenColor, 10);
        }

        private void DrawSceneGeometry()
        {
            DrawNode(Program.DocumentsManager.ActiveDocument.Model.RootNode, true);
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
