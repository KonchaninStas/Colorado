using Colorado.Documents.ModelStructure;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.Abstractions.Scene;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.Data
{
    internal interface IControllerInputData
    {
        IMousePositionInfo MousePositionInfo { get; }
        IViewport Viewport { get; }
        ICamera Camera { get; }
        IModel Model { get; }

        void SetCursorType(Cursor cursor);
    }

    internal class ControllerInputData : IControllerInputData
    {
        #region Private fields

        private readonly Control _control;

        #endregion Private fields

        #region Constructor

        public ControllerInputData(IMousePositionInfo mousePositionInfo, IRenderingControl renderingControl,
            Control control)
        {
            MousePositionInfo = mousePositionInfo;
            Viewport = renderingControl.Viewport;
            _control = control;
            Model = renderingControl.Program.DocumentsManager.ActiveDocument.Model;
            Camera = Viewport.Camera;
        }

        #endregion Constructor

        #region Properties

        public IMousePositionInfo MousePositionInfo { get; }

        public IViewport Viewport { get; }

        public ICamera Camera { get; }

        public IModel Model { get; }

        #endregion Properties

        #region Public logic

        public void SetCursorType(Cursor cursor)
        {
            _control.Cursor = cursor;
        }

        #endregion Public logic
    }
}
