using Colorado.Help.Keyboard;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers
{
    internal interface IController
    {
        string Name { get; }

        IEnumerable<IKeyboardCommand> KeyboardCommands { get; }

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
        public Controller()
        {
            KeyboardCommands = Enumerable.Empty<IKeyboardCommand>();
        }

        #region Properties

        public abstract string Name { get; }

        public virtual IEnumerable<IKeyboardCommand> KeyboardCommands { get; }

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
