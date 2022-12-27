using Colorado.Common.Logging;
using Colorado.Common.Services;
using Colorado.Common.WindowsLibrariesWrappers;
using Colorado.Documents;
using Colorado.Help.Keyboard;
using System;

using Strings = Colorado.Resources.Properties.Resources;

namespace Colorado.Application
{
    public interface IProgram
    {
        IDocumentsManager DocumentsManager { get; }
        ILogger Logger { get; }
        IWindowsLibrariesWrapper WindowsLibrariesWrapper { get; }
        IKeyboardCommandsManager KeyboardCommandsManager { get; }
    }

    public class Program : IProgram
    {
        #region Constructor

        public Program(IDocumentsManager documentsManager, ILogger logger,
            IWindowsLibrariesWrapper windowsLibrariesWrapper, IMessageBoxService messageBoxService,
            IKeyboardCommandsManager keyboardCommandsManager)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DocumentsManager = documentsManager;
            Logger = logger;
            WindowsLibrariesWrapper = windowsLibrariesWrapper;
            MessageBoxService = messageBoxService;
            KeyboardCommandsManager = keyboardCommandsManager;
        }

        #endregion Constructor

        #region Properties

        public IDocumentsManager DocumentsManager { get; }

        public ILogger Logger { get; }

        public IWindowsLibrariesWrapper WindowsLibrariesWrapper { get; }

        public IMessageBoxService MessageBoxService { get; }

        public IKeyboardCommandsManager KeyboardCommandsManager { get; }

        #endregion Properties

        #region Private logic

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            MessageBoxService.ShowExceptionMessage(Strings.UI_Title, exception.Message);
        }

        #endregion Private logic
    }
}
