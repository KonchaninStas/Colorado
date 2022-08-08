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

            Load += (s, e) =>
            {
                InitializeWindowStyles();
                _renderingControl.Initialize(Handle);
            };
            Paint += (s, e) => _renderingControl.DrawScene();
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
