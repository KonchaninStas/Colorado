namespace Colorado.Tools.SunPositionTool
{
    public class SunPosition
    {
        internal SunPosition(double altitude, double azimuth)
        {
            Altitude = altitude;
            Azimuth = azimuth;
        }

        public double Altitude { get; }
        public double Azimuth { get; }
    }
}
