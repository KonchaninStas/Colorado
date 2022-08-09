using Colorado.Geometry.Abstractions.Math;

namespace Colorado.Geometry.Structures.Math
{
    public class EulerAngles : IEulerAngles
    {
        public EulerAngles(double roll, double pitch, double yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        public double Roll { get; }

        public double Pitch { get; }

        public double Yaw { get; }
    }
}
