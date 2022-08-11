using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using System;

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

        void ResetToDefault();
        void SetDistanceToTarget(double distance);
        void Translate(IVector delta);
        void Refresh();
        void SetRefreshAction(Action refreshAction);
        void Zoom(double zoomFactor);
        void Pan(IPoint2D point2D);
        void RotateAroundTarget(IVector rotationAxis, double angleInDegrees);
        void RotateAroundTarget(IPoint2D from, IPoint2D to);
        void SetEyeTargetUp(IPoint newEye, IPoint newTarget, IVector newUp);

        IDefaultViewsManager DefaultViewsManager { get; }
    }

    public abstract class Camera : ICamera
    {
        private IPoint position;
        private Action _refreshView;

        public Camera()
        {
            CameraType = CameraType.Orthographic;
            DefaultViewsManager = new DefaultViewsManager(this);
        }

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

        public IDefaultViewsManager DefaultViewsManager { get; }

        protected ITransform GetViewMatrix()
        {
            return Transform.LookAt(Position, TargetPoint, UpVector);
        }

        public void ResetToDefault()
        {
            TargetPoint = new Point(0, 0, 0);
            Position = new Point(-1, 0, 0);
            UpVector = Vector.YAxis;
            SetDistanceToTarget(10);
        }

        public void SetEyeTargetUp(IPoint newEye, IPoint newTarget, IVector newUp)
        {
            Position = newEye;
            TargetPoint = newTarget;
            ITransform rotationTransform = Transform.CreateFromAxisAngle(DirectionVector.CrossProduct(newUp), Math.PI / 2);
            UpVector = rotationTransform.Apply(DirectionVector);
        }

        public void RotateAroundTarget(IPoint2D from, IPoint2D to)
        {
            double deltaX = to.X - from.X;
            double deltaY = to.Y - from.Y;
            if (deltaX != 0)
            {
                RotateAroundTarget(Vector.ZAxis, -deltaX / 5);
            }
            if (deltaY != 0)
            {
                RotateAroundTarget(RightVector, -deltaY / 5);
            }
        }

        public void RotateAroundTarget(IVector rotationAxis, double angleInDegrees)
        {
            RotateAroundTarget(Quaternion.Create(rotationAxis, angleInDegrees));
        }

        private void RotateAroundTarget(IQuaternion quaternion)
        {
            IVector newDirection = quaternion.ApplyToVector(DirectionVector);
            UpVector = quaternion.ApplyToVector(UpVector);
            Position = TargetPoint.Minus(newDirection.Multiply(FocalLength));
        }

        public void Translate(IVector delta)
        {
            Position = Position.Plus(delta);
            TargetPoint = TargetPoint.Plus(delta);
        }

        public void SetDistanceToTarget(double distance)
        {
            if (distance > 0)
            {
                Position = TargetPoint.Plus(DirectionVector.GetInversed().Multiply(distance));
            }
        }

        public void Refresh() => _refreshView?.Invoke();

        public void SetRefreshAction(Action refreshAction)
        {
            _refreshView = refreshAction;
        }

        public void Zoom(double zoomFactor)
        {
            SetDistanceToTarget(Math.Max(1, FocalLength * zoomFactor));
            Refresh();
        }

        public void Pan(IPoint2D cursorPosition)
        {
            IVector translateVector = RightVector.Multiply(cursorPosition.X).Plus(UpVector.Multiply(cursorPosition.Y));
            Translate(translateVector);
            Refresh();
        }
    }
}
