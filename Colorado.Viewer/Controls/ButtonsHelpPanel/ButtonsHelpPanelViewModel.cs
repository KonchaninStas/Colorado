using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Help.Keyboard;
using System.Collections.ObjectModel;

namespace Colorado.Viewer.Controls.ButtonsHelpPanel
{
    public interface IButtonsHelpPanelViewModel
    {
        ObservableCollection<IKeyboardCommand> ButtonHelpControls { get; }
    }

    public class ButtonsHelpPanelViewModel : ViewModelBase, IButtonsHelpPanelViewModel
    {
        public ButtonsHelpPanelViewModel(IKeyboardCommandsManager keyboardCommandsManager)
        {
            ButtonHelpControls = new ObservableCollection<IKeyboardCommand>();
            keyboardCommandsManager.CommandsListChanged += (s, a) => AddCommands((IKeyboardCommandsManager)s);
            AddCommands(keyboardCommandsManager);
        }

        private void AddCommands(IKeyboardCommandsManager keyboardCommandsManager)
        {
            ButtonHelpControls.Clear();
            foreach (IKeyboardCommand keyboardCommand in keyboardCommandsManager.Commands)
            {
                ButtonHelpControls.Add(keyboardCommand);
            }
        }

        public int ButtonHelpControlsCount => ButtonHelpControls.Count;

        public ObservableCollection<IKeyboardCommand> ButtonHelpControls { get; }
    }
}
