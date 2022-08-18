﻿using Colorado.Common.Utils;
using Colorado.Documents;
using Colorado.Geometry.Structures.Primitives;
using System;

namespace Colorado.Rendering.Controls.Abstractions.Scene
{
    public interface IViewport
    {
        double TargetPlaneWidth { get; }

        double TargetPlaneHeight { get; }
        double AspectRatio { get; }
        double FarClip { get; }
        int Height { get; }
        Vector2D ImageSize { get; }
        double NearClip { get; }
        double VerticalFieldOfViewInDegrees { get; set; }
        int Width { get; }
        ICamera Camera { get; }

        void SetViewportParameters(System.Drawing.Rectangle clientRectangle);

        void Apply();
        void ZoomToFit();
        Ray CalculateCursorRay(Point2D cursorPositionInScreenCoordinates);
    }

    public abstract class Viewport : IViewport
    {
        private readonly IDocumentsManager _documentsManager;

        private double verticalFieldOfViewInDegrees;

        public Viewport(ICamera camera, IDocumentsManager documentsManager)
        {
            Camera = camera;
            _documentsManager = documentsManager;
            ResetToDefault();
        }

        public ICamera Camera { get; }

        public double AspectRatio
        {
            get { return (Height == 0) ? 1.0 : Width / (double)Height; }
        }

        public Vector2D ImageSize
        {
            get
            {
                double imageY = 2.0 * Camera.FocalLength * Math.Tan(VerticalFieldOfViewInDegrees * Math.PI / 360);
                return new Vector2D(imageY * AspectRatio, imageY);
            }
        }

        public double NearClip
            => CalculateOrtoDistanceToModelCenter() - _documentsManager.ActiveDocument.Model.TotalBoundingBox.SphereRadius + 0.01;

        public double FarClip
            => CalculateOrtoDistanceToModelCenter() + _documentsManager.ActiveDocument.Model.TotalBoundingBox.SphereRadius;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public double VerticalFieldOfViewInDegrees
        {
            get
            {
                return verticalFieldOfViewInDegrees;
            }
            set
            {
                if (value > 0.0 && value < 180.0)
                {
                    verticalFieldOfViewInDegrees = value;
                }
            }
        }

        public double TargetPlaneWidth => TargetPlaneHeight * AspectRatio;

        public double TargetPlaneHeight => 2 * Camera.FocalLength *
            Math.Tan(MathUtils.Instance.ConvertDegreesToRadians(VerticalFieldOfViewInDegrees / 2));

        public void SetViewportParameters(System.Drawing.Rectangle clientRectangle)
        {
            Width = clientRectangle.Width;
            Height = clientRectangle.Height;
        }

        private double CalculateOrtoDistanceToModelCenter()
        {
            return _documentsManager.ActiveDocument.Model.TotalBoundingBox.Center.Inverse.DistanceTo(Camera.Position) *
                (_documentsManager.ActiveDocument.Model.TotalBoundingBox.Center.Inverse - Camera.Position).UnitVector.DotProduct(Camera.DirectionVector);
        }

        public abstract void Apply();

        private void ResetToDefault()
        {
            VerticalFieldOfViewInDegrees = 45;
            Camera.ResetToDefault();
        }

        public void ZoomToFit()
        {
            var distanse = _documentsManager.ActiveDocument.Model.TotalBoundingBox.SphereRadius *
                (1.0 / Math.Tan(MathUtils.Instance.ConvertDegreesToRadians(VerticalFieldOfViewInDegrees / 2)));
            Camera.Translate(new Vector(Camera.TargetPoint, _documentsManager.ActiveDocument.Model.TotalBoundingBox.Center.Inverse));
            Camera.SetDistanceToTarget(distanse);
            Camera.Refresh();
        }

        public abstract Ray CalculateCursorRay(Point2D cursorPositionInScreenCoordinates);
    }
}
