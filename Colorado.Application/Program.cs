using Colorado.Common.Logging;
using Colorado.Common.WindowsLibrariesWrappers;
using Colorado.Common.WindowsLibrariesWrappers.Gdi32;
using Colorado.Common.WindowsLibrariesWrappers.Kernel32;
using Colorado.Common.WindowsLibrariesWrappers.User32;
using Colorado.Documents;
using Colorado.Documents.Readers.STLDocumentReader;
using System;
using System.Windows;

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
        //#region Singelton

        //private static IProgram _instance;

        //public static IProgram Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new Program();
        //        }
        //        return _instance;
        //    }
        //}

        //#endregion Singelton

        public Program()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DocumentsManager = new DocumentsManager();
            DocumentsManager.RegisterFileReader(new STLDocumentReader());

            Logger = new Logger();

            WindowsLibrariesWrapper = new WindowsLibrariesWrapper(new Gdi32LibraryWrapper(),
                new Kernel32LibraryWrapper(), new User32LibraryWrapper());
        }

        public IDocumentsManager DocumentsManager { get; }

        public ILogger Logger { get; }

        public IWindowsLibrariesWrapper WindowsLibrariesWrapper { get; }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).Message);
        }
    }
}
