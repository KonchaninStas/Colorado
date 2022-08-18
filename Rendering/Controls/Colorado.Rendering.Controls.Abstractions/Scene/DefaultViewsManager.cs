using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene.Enumerations;

namespace Colorado.Rendering.Controls.Abstractions.Scene
{
    public interface IDefaultViewsManager
    {
        void SetDefaultCameraView(DefaultCameraView defaultCameraView);
    }

    public class DefaultViewsManager : IDefaultViewsManager
    {
        #region Private fields

        private readonly ICamera camera;

        #endregion Private fields

        #region Constructors

        public DefaultViewsManager(ICamera camera)
        {
            this.camera = camera;
        }

        #endregion Constructors

        #region Public logic

        public void SetDefaultCameraView(DefaultCameraView defaultCameraView)
        {
            switch (defaultCameraView)
            {
                case DefaultCameraView.Front:
                    SetFrontView();
                    break;
                case DefaultCameraView.Top:
                    SetTopView();
                    break;
                case DefaultCameraView.Rear:
                    SetRearView();
                    break;
                case DefaultCameraView.Right:
                    SetRightView();
                    break;
                case DefaultCameraView.Left:
                    SetLeftView();
                    break;
                case DefaultCameraView.Bottom:
                    SetBottomView();
                    break;
                case DefaultCameraView.Iso:
                    SetIsoView();
                    break;
                default:
                    break;
            }
            camera.Refresh();
        }

        #endregion Public logic

        #region Private logic

        private void SetFrontView()
        {
            SetView(new Vector(1, 0, 0), new Vector(0, 0, 1));
        }

        private void SetRearView()
        {
            SetView(new Vector(-1, 0, 0), new Vector(0, 0, 1));
        }

        private void SetRightView()
        {
            SetView(new Vector(0, 1, 0), new Vector(0, 0, 1));
        }

        private void SetLeftView()
        {
            SetView(new Vector(0, -1, 0), new Vector(0, 0, 1));
        }

        private void SetTopView()
        {
            SetView(new Vector(0, 0, 1), new Vector(-1, 0, 0));
        }

        private void SetBottomView()
        {
            SetView(new Vector(0, 0, -1), new Vector(-1, 0, 0));
        }

        private void SetIsoView()
        {
            SetFrontView();
            camera.RotateAroundTarget(Vector.ZAxis, 45);
            camera.RotateAroundTarget(camera.RightVector, -45);
        }

        private void SetView(Vector direction, Vector newUp)
        {
            double distance = camera.FocalLength;
            camera.SetEyeTargetUp(camera.TargetPoint - direction, camera.TargetPoint, newUp);
            camera.SetDistanceToTarget(distance);
        }

        #endregion Private logic
    }
}
