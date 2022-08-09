using Microsoft.Extensions.DependencyInjection;

namespace Colorado.Services.Math
{
    public interface IMathService
    {
        double Clamp(double value, double min, double max);
        double ConvertDegreesToRadians(double degrees);
        double ConvertRadiansToDegrees(double radians);
    }

    public class MathService : IMathService
    {
        public static IMathService Instance => ServiceManager.Instance.ServiceProvider.GetService<IMathService>();

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
