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
        private static ILogger _instance;
        public static ILogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }

                return _instance;
            }
        }

        private Logger() { }

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
