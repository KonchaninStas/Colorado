using Colorado.Rendering.Controls.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            InitializeWindowStyles();
            _renderingControl.Initialize(Handle);
        }

        private void WinFormsRenderingControl_Disposed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
