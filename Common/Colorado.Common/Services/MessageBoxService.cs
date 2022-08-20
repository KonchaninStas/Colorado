using System;

namespace Colorado.Common.Services
{
    public interface IMessageBoxService
    {
        void ShowInformationMessage(string title, string message);
        void ShowExceptionMessage(string title, string message);
        void ShowExceptionMessage(string title, string message, Exception ex);
    }

    public abstract class MessageBoxService : IMessageBoxService
    {
        public abstract void ShowInformationMessage(string title, string message);

        public abstract void ShowExceptionMessage(string title, string message, Exception ex);

        public abstract void ShowExceptionMessage(string title, string message);
    }
}
