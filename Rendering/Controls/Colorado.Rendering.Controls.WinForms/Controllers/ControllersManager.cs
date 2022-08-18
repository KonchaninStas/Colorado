using Colorado.Common.Extensions;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using Colorado.Rendering.Controls.WinForms.Controllers.Exceptions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers
{
    internal class ControllersManager
    {
        #region private fields

        private readonly Control control;
        private readonly IRenderingControl renderingControl;

        private readonly Dictionary<string, IController> controllerNameToControllerMap;
        private readonly ICollection<IController> controllersToAttach;
        private readonly ICollection<string> controllersToRemove;

        private IMousePositionInfo lastMousePositionInfo;

        #endregion Private fields

        #region Constructor

        private ControllersManager(Control control, IRenderingControl renderingControl)
        {
            this.control = control;
            this.renderingControl = renderingControl;
            controllerNameToControllerMap = new Dictionary<string, IController>();
            controllersToAttach = new List<IController>();
            controllersToRemove = new List<string>();
            SubscribeToEvents();
        }

        #endregion Constructor

        #region Singelton

        public static ControllersManager Instance { get; private set; }

        internal static void Register(Control control, IRenderingControl renderingControl)
        {
            Instance = new ControllersManager(control, renderingControl);
        }

        internal static void Unregister()
        {
            Instance?.UnsubscribeFromEvents();
        }

        #endregion Singelton

        #region Initialization/Destroying

        private void SubscribeToEvents()
        {
            control.MouseMove += MouseMoveCallback;
            control.MouseDown += MouseDownCallback;
            control.MouseUp += MouseUpCallback;
            control.MouseWheel += MouseWheelCallback;
            control.PreviewKeyDown += PreviewKeyDownCallback;
            control.KeyDown += KeyDownCallback;
            control.KeyUp += KeyUpCallback;
        }

        private void UnsubscribeFromEvents()
        {
            control.MouseMove -= MouseMoveCallback;
            control.MouseDown -= MouseDownCallback;
            control.MouseUp -= MouseUpCallback;
            control.MouseWheel -= MouseWheelCallback;
            control.PreviewKeyDown -= PreviewKeyDownCallback;
            control.KeyDown -= KeyDownCallback;
            control.KeyUp -= KeyUpCallback;
        }

        #endregion Initialization/Destroying

        #region Callbacks

        private void MouseMoveCallback(object sender, MouseEventArgs e)
        {
            lastMousePositionInfo = new MousePositionInfo(e, renderingControl.Viewport);
            controllerNameToControllerMap.Values.ForEach(
                c => c.OnMouseMove(e.Button, GetControllerInputData()));
            control.Refresh();
        }

        private void MouseDownCallback(object sender, MouseEventArgs e)
        {
            controllerNameToControllerMap.Values.ForEach(
                c => c.OnMouseDown(e.Button, GetControllerInputData()));
            CallBackPostProcess();
        }

        private void MouseUpCallback(object sender, MouseEventArgs e)
        {
            controllerNameToControllerMap.Values.ForEach(
                c => c.OnMouseUp(e.Button, GetControllerInputData()));
            control.Refresh();
        }

        private void MouseWheelCallback(object sender, MouseEventArgs e)
        {
            controllerNameToControllerMap.Values.ForEach(
                c => c.OnMouseWheel(e.Delta, GetControllerInputData()));
            control.Refresh();
        }

        private void PreviewKeyDownCallback(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void KeyUpCallback(object sender, KeyEventArgs e)
        {
            controllerNameToControllerMap.Values.ForEach(
                c => c.OnKeyUp(e.KeyCode, GetControllerInputData()));
            control.Refresh();
        }

        private void KeyDownCallback(object sender, KeyEventArgs e)
        {
            controllerNameToControllerMap.Values.ForEach(
                c => c.OnKeyDown(e.KeyCode, GetControllerInputData()));
            CallBackPostProcess();
        }

        private ControllerInputData GetControllerInputData()
        {
            return new ControllerInputData(lastMousePositionInfo, renderingControl, control);
        }

        private void CallBackPostProcess()
        {
            RemoveDeferredControllers();
            AddDeferredControllers();
            control.Refresh();
        }

        #endregion Callbacks

        #region Public logic

        public void AddControllers(IEnumerable<IController> controllers)
        {
            controllers.ForEach(c => AddController(c));
        }

        public void AddController(IController controller)
        {
            if (controllerNameToControllerMap.ContainsKey(controller.Name))
            {
                throw new ControllerIsAddedException();
            }
            else
            {
                controllerNameToControllerMap.Add(controller.Name, controller);
            }
        }

        public void AddControllerDeferred(IController controller)
        {
            controllersToAttach.Add(controller);
        }

        public void RemoveController(string controllerName)
        {
            if (controllerNameToControllerMap.ContainsKey(controllerName))
            {
                controllerNameToControllerMap.Remove(controllerName);
            }
            else
            {
                throw new ControllerIsNotRegisteredException();
            }
        }

        public void RemoveControllerDeferred(string controllerName)
        {
            controllersToRemove.Add(controllerName);
        }

        #endregion Public logic

        #region Internal logic

        internal void DrawPrimitives(IGeometryRenderer geometryRenderer)
        {
            controllerNameToControllerMap.Values.ForEach(c => c.DrawPrimitives(geometryRenderer));
        }

        #endregion Internal logic

        #region Private logic

        private void AddDeferredControllers()
        {
            foreach (IController controller in controllersToAttach)
            {
                if (!controllerNameToControllerMap.ContainsKey(controller.Name))
                {
                    AddController(controller);
                }
            }
            controllersToAttach.Clear();
        }

        private void RemoveDeferredControllers()
        {
            foreach (string controllerName in controllersToRemove)
            {
                if (controllerNameToControllerMap.ContainsKey(controllerName))
                {
                    RemoveController(controllerName);
                }
            }
            controllersToRemove.Clear();
        }

        #endregion Private logic
    }
}
