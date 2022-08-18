using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;
using Colorado.Rendering.Controls.Abstractions.Utils;
using System;

namespace Colorado.Rendering.Controls.Abstractions.Scene
{
    public interface ICamera
    {
        #region Properties

        Vector DirectionVector { get; }
        Point Position { get; }
        Vector RightVector { get; }
        Point TargetPoint { get; }
        Vector UpVector { get; }
        CameraType CameraType { get; set; }
        double FocalLength { get; }
        IDefaultViewsManager DefaultViewsManager { get; }
        Plane TargetPointPlane { get; }

        #endregion Properties

        #region Methods

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

        #endregion Methods
    }

    public abstract class Camera : ICamera
    {
        #region Private fields

        private readonly ITotalBoundingBoxProvider _totalBoundingBoxProvider;

        private Point position;
        private Action _refreshView;

        #endregion Private fields

        #region Constructor

        public Camera(ITotalBoundingBoxProvider totalBoundingBoxProvider)
        {
            _totalBoundingBoxProvider = totalBoundingBoxProvider;
            CameraType = CameraType.Orthographic;
            DefaultViewsManager = new DefaultViewsManager(this);
        }

        #endregion Constructor

        #region Properties

        public Plane TargetPointPlane => new Plane(TargetPoint, DirectionVector);

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

        #endregion Properties

        #region Public logic

        public void ResetToDefault()
        {
            TargetPoint = new Point(0, 0, 0);
            Position = new Point(0, 0, 1);
            UpVector = Vector.YAxis;
            SetDistanceToTarget(10);
            DefaultViewsManager.SetDefaultCameraView(Enumerations.DefaultCameraView.Front);
            RotateAroundTarget(Vector.ZAxis, 45);
            RotateAroundTarget(RightVector, -30);
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
                RotateAroundTarget(Vector.ZAxis, -deltaX / 5);
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

        public void Translate(Vector delta)
        {
            Position = Position + delta;
            TargetPoint = TargetPoint + delta;
        }

        public void SetDistanceToTarget(double distance)
        {
            if (distance > _totalBoundingBoxProvider.NodesBoundingBox.Diagonal * 0.1 &&
                distance < _totalBoundingBoxProvider.TotalBoundingBox.Diagonal * 2)
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
            Translate(TargetPointPlane.GetIntersectionPoint(mouseRay) - TargetPoint);
            Zoom(zoomFactor);
        }

        public void Pan(Point2D cursorPosition)
        {
            Vector translateVector = RightVector * cursorPosition.X + (UpVector * cursorPosition.Y);
            Translate(translateVector);
            Refresh();
        }

        #endregion Public logic

        #region Protected logic

        protected ITransform GetViewMatrix()
        {
            return Transform.LookAt(Position, TargetPoint, UpVector);
        }

        #endregion Protected logic

        #region Private logic

        private void RotateAroundTarget(IQuaternion quaternion)
        {
            Vector newDirection = quaternion.ApplyToVector(DirectionVector);
            UpVector = quaternion.ApplyToVector(UpVector);
            Position = TargetPoint - (newDirection * FocalLength);
        }

        #endregion Private logic
    }
}
