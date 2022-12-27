using Colorado.Help.Keyboard;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal abstract class BaseKeyController : Controller
    {
        private readonly Dictionary<Keys, Action<IControllerInputData>> _keyCodeToActionMap;
        private readonly ICollection<IKeyboardCommand> _keyboardCommands;

        protected BaseKeyController()
        {
            _keyCodeToActionMap = new Dictionary<Keys, Action<IControllerInputData>>();
            _keyboardCommands = new List<IKeyboardCommand>();
        }

        public override IEnumerable<IKeyboardCommand> KeyboardCommands => _keyboardCommands;

        public override void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData)
        {
            GetCommandCallback(keyCode)?.Invoke(controllerInputData);
        }

        public override void OnKeyUp(Keys keyCode, IControllerInputData controllerInputData)
        {
            //GetCommandCallback(keyCode)?.Invoke(controllerInputData);
        }

        protected void AddCommand(IKeyboardCommand keyboardCommand, Action<IControllerInputData> callback)
        {
            _keyCodeToActionMap[keyboardCommand.Key] = callback;
            _keyboardCommands.Add(keyboardCommand);
        }

        private Action<IControllerInputData> GetCommandCallback(Keys commandKey)
        {
            if (_keyCodeToActionMap.TryGetValue(commandKey, out Action<IControllerInputData> callback))
            {
                return callback;
            }
            else
            {
                return null;
            }
        }
    }
}
