using Colorado.Common.Utils;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Utils;
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
        private readonly ITotalBoundingBoxProvider _totalBoundingBoxProvider;
        private double verticalFieldOfViewInDegrees;

        public Viewport(ICamera camera, ITotalBoundingBoxProvider totalBoundingBoxProvider)
        {
            Camera = camera;
            _totalBoundingBoxProvider = totalBoundingBoxProvider;
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
        {
            get
            {
                double distanceToModelCenter = CalculateOrtoDistanceToModelCenter();
                double result = distanceToModelCenter - _totalBoundingBoxProvider.TotalBoundingBox.SphereRadius + 0.01;

                return result < 0.01 ? 0.01 : result;
            }
        }

        public double FarClip
            => CalculateOrtoDistanceToModelCenter() + _totalBoundingBoxProvider.TotalBoundingBox.SphereRadius;

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
            return _totalBoundingBoxProvider.TotalBoundingBox.Center.Inverse.DistanceTo(Camera.Position) *
                (_totalBoundingBoxProvider.TotalBoundingBox.Center.Inverse - Camera.Position).UnitVector.DotProduct(Camera.DirectionVector);
        }

        public abstract void Apply();

        private void ResetToDefault()
        {
            VerticalFieldOfViewInDegrees = 45;
            Camera.ResetToDefault();
        }

        public void ZoomToFit()
        {
            var distanse = _totalBoundingBoxProvider.NodesBoundingBox.SphereRadius *
                (1.0 / Math.Tan(MathUtils.Instance.ConvertDegreesToRadians(VerticalFieldOfViewInDegrees / 2)));
            Camera.Translate(new Vector(Camera.TargetPoint, _totalBoundingBoxProvider.NodesBoundingBox.Center.Inverse));
            Camera.SetDistanceToTarget(distanse);
            Camera.Refresh();
        }

        public abstract Ray CalculateCursorRay(Point2D cursorPositionInScreenCoordinates);
    }
}
