using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions;
using System;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms
{
    public partial class WinFormsRenderingControl : UserControl
    {
        private readonly IRenderingControl _renderingControl;

        public WinFormsRenderingControl(IRenderingControl renderingControl)
        {
            InitializeComponent();

            Disposed += (s, args) => _renderingControl.Dispose();
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
            MouseWheel += WinFormsRenderingControl_MouseWheel;
            KeyDown += WinFormsRenderingControl_KeyDown;
        }

        private void WinFormsRenderingControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _renderingControl.Viewport.Camera.Translate(_renderingControl.Viewport.Camera.UpVector.GetInversed());
                    break;
                case Keys.A:
                    _renderingControl.Viewport.Camera.Translate(_renderingControl.Viewport.Camera.RightVector.GetInversed());
                    break;
                case Keys.S:
                    _renderingControl.Viewport.Camera.Translate(_renderingControl.Viewport.Camera.UpVector);
                    break;
                case Keys.D:
                    _renderingControl.Viewport.Camera.Translate(_renderingControl.Viewport.Camera.RightVector);
                    break;
                default:
                    break;
            }
            _renderingControl.Viewport.Camera.Refresh();
        }

        private void WinFormsRenderingControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                _renderingControl.Viewport.Camera.Zoom(1.5);
            }
            else
            {
                _renderingControl.Viewport.Camera.Zoom(0.5);
            }
        }

        private void WinFormsRenderingControl_Disposed(object sender, EventArgs e)
        {
            _renderingControl.Dispose();
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
