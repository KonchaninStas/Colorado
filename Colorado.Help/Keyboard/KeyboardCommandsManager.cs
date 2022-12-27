using Colorado.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Colorado.Help.Keyboard
{
    public interface IKeyboardCommandsManager
    {
        IEnumerable<IKeyboardCommand> Commands { get; }

        event EventHandler CommandsListChanged;

        void AddCommand(IKeyboardCommand keyboardCommand);
        void RemoveCommand(IKeyboardCommand keyboardCommand);

        void AddCommands(IEnumerable<IKeyboardCommand> keyboardCommands);
        void RemoveCommands(IEnumerable<IKeyboardCommand> keyboardCommands);
    }

    public class KeyboardCommandsManager : IKeyboardCommandsManager
    {
        private readonly HashSet<IKeyboardCommand> _registeredCommands;
        private readonly ObservableCollection<IKeyboardCommand> _commands;

        private static KeyboardCommandsManager _instance;
        public static IKeyboardCommandsManager Instance => _instance ?? (_instance = new KeyboardCommandsManager());

        private KeyboardCommandsManager()
        {
            _registeredCommands = new HashSet<IKeyboardCommand>();
            _commands = new ObservableCollection<IKeyboardCommand>();
            _commands.CollectionChanged += (s, a) => CommandsListChanged?.Invoke(this, EventArgs.Empty);
        }

        public IEnumerable<IKeyboardCommand> Commands => _commands;

        public event EventHandler CommandsListChanged;

        public void AddCommand(IKeyboardCommand keyboardCommand)
        {
            if (_registeredCommands.Add(keyboardCommand))
            {
                _commands.Add(keyboardCommand);
            }
        }

        public void RemoveCommand(IKeyboardCommand keyboardCommand)
        {
            if (_registeredCommands.Remove(keyboardCommand))
            {
                _commands.Remove(keyboardCommand);
            }
        }

        public void AddCommands(IEnumerable<IKeyboardCommand> keyboardCommands)
        {
            keyboardCommands.ForEach(c => AddCommand(c));
        }

        public void RemoveCommands(IEnumerable<IKeyboardCommand> keyboardCommands)
        {
            keyboardCommands.ForEach(c => RemoveCommand(c));
        }
    }
}
