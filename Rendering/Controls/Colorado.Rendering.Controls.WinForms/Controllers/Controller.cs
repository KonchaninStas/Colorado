using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers
{
    internal interface IController
    {
        string Name { get; }

        void DrawPrimitives(IGeometryRenderer geometryRenderer);
        void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData);
        void OnKeyUp(Keys keyCode, IControllerInputData controllerInputData);
        void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData);
        void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData);
        void OnMouseUp(MouseButtons button, IControllerInputData controllerInputData);
        void OnMouseWheel(int delta, IControllerInputData controllerInputData);
    }

    internal abstract class Controller : IController
    {
        #region Properties

        public abstract string Name { get; }

        #endregion Properties

        #region Public logic

        public virtual void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData) { }

        public virtual void OnKeyUp(Keys keyCode, IControllerInputData controllerInputData) { }

        public virtual void OnMouseWheel(int delta, IControllerInputData controllerInputData) { }

        public virtual void OnMouseUp(MouseButtons button, IControllerInputData controllerInputData) { }

        public virtual void OnMouseMove(MouseButtons button, IControllerInputData controllerInputData) { }

        public virtual void OnMouseDown(MouseButtons button, IControllerInputData controllerInputData) { }

        public virtual void DrawPrimitives(IGeometryRenderer geometryRenderer) { }

        #endregion Public logic
    }
}
