using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using System;

namespace Colorado.Rendering.Controls.Abstractions.Scene
{
    public interface ICamera
    {
        Vector DirectionVector { get; }
        Point Position { get; }
        Vector RightVector { get; }
        Point TargetPoint { get; }
        Vector UpVector { get; }
        CameraType CameraType { get; set; }
        double FocalLength { get; }

        void ResetToDefault();
        void SetDistanceToTarget(double distance);
        void Translate(Vector delta);
        void Refresh();
        void SetRefreshAction(Action refreshAction);
        void Zoom(double zoomFactor);
        void Pan(Point2D point2D);
        void RotateAroundTarget(Vector rotationAxis, double angleInDegrees);
        void RotateAroundTarget(Point2D from, Point2D to);
        void SetEyeTargetUp(Point newEye, Point newTarget, Vector newUp);

        IDefaultViewsManager DefaultViewsManager { get; }
    }

    public abstract class Camera : ICamera
    {
        private Point position;
        private Action _refreshView;

        public Camera()
        {
            CameraType = CameraType.Orthographic;
            DefaultViewsManager = new DefaultViewsManager(this);
        }

        public CameraType CameraType { get; set; }

        public double FocalLength => Position.DistanceTo(TargetPoint);

        public Point Position
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

        public Point TargetPoint { get; private set; }

        public Vector DirectionVector => new Vector(Position, TargetPoint).UnitVector;

        public Vector RightVector => UpVector.CrossProduct(DirectionVector).UnitVector;

        public Vector UpVector { get; private set; }

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
            DefaultViewsManager.SetDefaultCameraView(Enumerations.DefaultCameraView.Iso);
        }

        public void SetEyeTargetUp(Point newEye, Point newTarget, Vector newUp)
        {
            Position = newEye;
            TargetPoint = newTarget;
            UpVector = newUp;
        }

        public void RotateAroundTarget(Point2D from, Point2D to)
        {
            double deltaX = to.X - from.X;
            double deltaY = to.Y - from.Y;
            if (deltaX != 0)
            {
                RotateAroundTarget(UpVector, -deltaX / 5);
            }
            if (deltaY != 0)
            {
                RotateAroundTarget(RightVector, -deltaY / 5);
            }
        }

        public void RotateAroundTarget(Vector rotationAxis, double angleInDegrees)
        {
            RotateAroundTarget(Quaternion.Create(rotationAxis, angleInDegrees));
        }

        private void RotateAroundTarget(IQuaternion quaternion)
        {
            Vector newDirection = quaternion.ApplyToVector(DirectionVector);
            UpVector = quaternion.ApplyToVector(UpVector);
            Position = TargetPoint - (newDirection * FocalLength);
        }

        public void Translate(Vector delta)
        {
            Position = Position + delta;
            TargetPoint = TargetPoint + delta;
        }

        public void SetDistanceToTarget(double distance)
        {
            if (distance > 0)
            {
                Position = TargetPoint + (DirectionVector.Inversed * distance);
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

        public void ZoomToPoint(double zoomFactor, Ray mouseRay)
        {
            //Translate(translationVector);
            SetDistanceToTarget(Math.Max(1, FocalLength * zoomFactor));
            Refresh();
        }

        public void Pan(Point2D cursorPosition)
        {
            Vector translateVector = RightVector * cursorPosition.X + (UpVector * cursorPosition.Y);
            Translate(translateVector);
            Refresh();
        }
    }
}
