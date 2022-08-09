using Colorado.ModelStructure;
using Colorado.Rendering.Controls.Abstractions;
using System;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms
{
    public partial class WinFormsRenderingControl : UserControl
    {
        private readonly IRenderingControl _renderingControl;
        private readonly IModel _model;

        public WinFormsRenderingControl(IRenderingControl renderingControl, IModel model)
        {
            InitializeComponent();
            _model = model;

            Disposed += (s, args) => _renderingControl.Dispose();
            _renderingControl = renderingControl;

            Load += (s, e) =>
            {
                InitializeWindowStyles();
                _renderingControl.Initialize(Handle);
            };
            Paint += (s, e) => DrawScene();
            SizeChanged += (s, e) => _renderingControl.Viewport.SetViewportParameters(ClientRectangle);
        }

        private void WinFormsRenderingControl_Disposed(object sender, EventArgs e)
        {
            _renderingControl.Dispose();
        }

        private void DrawScene()
        {
            _renderingControl.BeforeDrawScene();

            _renderingControl.Viewport.Apply();

            _renderingControl.DrawSceneGeometry();
            _renderingControl.DisableLighting();
            _renderingControl.DrawScenePrimitives();

            _renderingControl.EndDrawScene();
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
