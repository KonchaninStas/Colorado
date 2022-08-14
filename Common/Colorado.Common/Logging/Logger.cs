using System;
using System.Diagnostics;

namespace Colorado.Common.Logging
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogError(Exception ex);
    }

    public class Logger : ILogger
    {
        public Logger() { }

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
