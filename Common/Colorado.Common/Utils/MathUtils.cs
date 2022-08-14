namespace Colorado.Common.Utils
{
    public interface IMathUtils
    {
        double Clamp(double value, double min, double max);
        double ConvertDegreesToRadians(double degrees);
        double ConvertRadiansToDegrees(double radians);
    }

    public class MathUtils : IMathUtils
    {
        private static IMathUtils _instance;
        public static IMathUtils Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MathUtils();
                }

                return _instance;
            }
        }

        private MathUtils() { }

        public double ConvertRadiansToDegrees(double radians)
        {
            return 180 / System.Math.PI * radians;
        }

        public double ConvertDegreesToRadians(double degrees)
        {
            return System.Math.PI / 180 * degrees;
        }

        public double Clamp(double value, double min, double max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }
    }
}
