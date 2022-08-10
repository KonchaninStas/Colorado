using Colorado.Geometry.Abstractions.BoundingBoxStructures;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.ModelStructure;
using Colorado.Services.Math;
using System;

namespace Colorado.Rendering.Controls.Abstractions.Scene
{
    public interface IViewport
    {
        double AspectRatio { get; }
        double FarClip { get; }
        int Height { get; }
        IVector2D ImageSize { get; }
        double NearClip { get; }
        double VerticalFieldOfViewInDegrees { get; set; }
        int Width { get; }
        ICamera Camera { get; }

        void SetViewportParameters(System.Drawing.Rectangle clientRectangle);

        void Apply();
        void ZoomToFit();
    }

    public abstract class Viewport : IViewport
    {
        private readonly IModel _model;

        private double verticalFieldOfViewInDegrees;

        public Viewport(ICamera camera, IModel model)
        {
            Camera = camera;
            _model = model;
            ResetToDefault();
        }

        public ICamera Camera { get; }

        public double AspectRatio
        {
            get { return (Height == 0) ? 1.0 : Width / (double)Height; }
        }

        public IVector2D ImageSize
        {
            get
            {
                double imageY = 2.0 * Camera.FocalLength * Math.Tan(VerticalFieldOfViewInDegrees * Math.PI / 360);
                return new Vector2D(imageY * AspectRatio, imageY);
            }
        }

        public double NearClip
        {
            get
            {
                var t = CalculateOrtoDistanceToModelCenter() - _model.TotalBoundingBox.SphereRadius + 0.01;

                return t;
            }
        }

        public double FarClip
        {
            get
            {
                var t = CalculateOrtoDistanceToModelCenter() + _model.TotalBoundingBox.SphereRadius;

                return t;
            }
        }

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

        public void SetViewportParameters(System.Drawing.Rectangle clientRectangle)
        {
            Width = clientRectangle.Width;
            Height = clientRectangle.Height;
        }

        private double CalculateOrtoDistanceToModelCenter()
        {
            return _model.TotalBoundingBox.Center.DistanceTo(Camera.Position.Inverse) *
                _model.TotalBoundingBox.Center.Inverse.Minus(Camera.Position).UnitVector.DotProduct(Camera.DirectionVector);
        }

        public abstract void Apply();

        private void ResetToDefault()
        {
            VerticalFieldOfViewInDegrees = 45;
            Camera.ResetToDefault();
        }

        public void ZoomToFit()
        {
            var distanse = _model.TotalBoundingBox.SphereRadius *
                (1.0 / Math.Tan(MathService.Instance.ConvertDegreesToRadians(VerticalFieldOfViewInDegrees / 2)));
            Camera.Translate(new Vector(_model.TotalBoundingBox.Center, Camera.TargetPoint));
            Camera.SetDistanceToTarget(distanse);
            Camera.Refresh();
        }
    }
}
