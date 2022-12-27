using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.WinForms.Controllers;
using Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers;
using Colorado.Rendering.Controls.WinForms.Controllers.MouseControllers;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms
{
    public partial class WinFormsRenderingControl : UserControl
    {
        private readonly IRenderingControl _renderingControl;

        public WinFormsRenderingControl(IRenderingControl renderingControl)
        {
            InitializeComponent();

            Disposed += (s, args) =>
            {
                ControllersManager.Unregister();
                _renderingControl.Dispose();
            };
            _renderingControl = renderingControl;
            _renderingControl.Viewport.Camera.SetRefreshAction(() => Refresh());

            Load += (s, e) =>
            {
                InitializeWindowStyles();
                _renderingControl.Initialize(Handle);
                renderingControl.Viewport.ZoomToFit();
                Refresh();
            };

            Paint += (s, e) => _renderingControl.DrawScene();
            SizeChanged += (s, e) => _renderingControl.Viewport.SetViewportParameters(ClientRectangle);


            ControllersManager.Register(this, _renderingControl);

            ControllersManager.Instance.AddController(new ZoomMouseController());
            ControllersManager.Instance.AddController(new PanMouseController());
            ControllersManager.Instance.AddController(new CameraKeyController());
            ControllersManager.Instance.AddController(new ModelKeyController());
            ControllersManager.Instance.AddController(new OrbitMouseController());
            //ControllersManager.Instance.AddController(new RotationMouseController()); TODO works wrong
            ControllersManager.Instance.AddController(new DefaultViewSwitchingController());
        }

        private void InitializeWindowStyles()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
        }
    }
}
