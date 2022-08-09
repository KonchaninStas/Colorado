using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;

namespace Colorado.Rendering.Controls.Abstractions.Scene
{
    public interface ICamera
    {
        IVector DirectionVector { get; }
        IPoint Position { get; }
        IVector RightVector { get; }
        IPoint TargetPoint { get; }
        IVector UpVector { get; }
        CameraType CameraType { get; set; }
        double FocalLength { get; }
    }

    public abstract class Camera : ICamera
    {
        private IPoint position;

        public CameraType CameraType { get; set; }

        public double FocalLength => Position.DistanceTo(TargetPoint);

        public IPoint Position
        {
            get
            {
                return position;
            }
            private set
            {
                if (value.DistanceTo(TargetPoint) > 0.1)
                {
                    position = value;
                }
            }
        }

        public IPoint TargetPoint { get; private set; }

        public IVector DirectionVector => new Vector(Position, TargetPoint).UnitVector;

        public IVector RightVector => UpVector.CrossProduct(DirectionVector).UnitVector;

        public IVector UpVector { get; private set; }

        protected ITransform GetViewMatrix()
        {
            return Transform.LookAt(Position, TargetPoint, UpVector);
        }
    }
}
