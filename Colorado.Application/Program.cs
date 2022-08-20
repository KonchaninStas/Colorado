using Colorado.Common.Logging;
using Colorado.Common.Services;
using Colorado.Common.WindowsLibrariesWrappers;
using Colorado.Documents;
using System;

using Strings = Colorado.Resources.Properties.Resources;

namespace Colorado.Application
{
    public interface IProgram
    {
        IDocumentsManager DocumentsManager { get; }
        ILogger Logger { get; }
        IWindowsLibrariesWrapper WindowsLibrariesWrapper { get; }
    }

    public class Program : IProgram
    {
        #region Constructor

        public Program(IDocumentsManager documentsManager, ILogger logger,
            IWindowsLibrariesWrapper windowsLibrariesWrapper, IMessageBoxService messageBoxService)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DocumentsManager = documentsManager;
            Logger = logger;
            WindowsLibrariesWrapper = windowsLibrariesWrapper;
            MessageBoxService = messageBoxService;
        }

        #endregion Constructor

        #region Properties

        public IDocumentsManager DocumentsManager { get; }

        public ILogger Logger { get; }

        public IWindowsLibrariesWrapper WindowsLibrariesWrapper { get; }

        public IMessageBoxService MessageBoxService { get; }

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
