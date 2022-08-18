using System.Diagnostics;

namespace Colorado.Rendering.Controls.Abstractions.Utils
{
    public static class ApplicationTimer
    {
        private readonly static Stopwatch _Stopwatch = Stopwatch.StartNew();

        /// <summary>
        /// Gets the system time in seconds (double-precision)
        /// </summary>
        /// <value>The system time in seconds.</value>
        static public double SysTime
        {
            get
            {
                return ((double)_Stopwatch.ElapsedMilliseconds) / 1000.0;
            }
        }

    }
}
