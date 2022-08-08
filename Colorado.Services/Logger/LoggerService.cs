using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Colorado.Services.Logger
{
    public interface ILoggerService
    {
        void LogDebug(string message);
        void LogError(Exception ex);
    }

    public class LoggerService : ILoggerService
    {
        public static ILoggerService Instance => ServiceManager.Instance.ServiceProvider.GetService<ILoggerService>();

        public void LogDebug(string message)
        {
            Debug.WriteLine(message);
        }

        public void LogError(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
