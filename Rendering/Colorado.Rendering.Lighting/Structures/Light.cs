using Colorado.Common.Colours;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;

namespace Colorado.Rendering.Lighting.Structures
{
    public interface ILight
    {
        double AltitudeAngleInDegrees { get; set; }
        RGB Ambient { get; set; }
        double AzimuthAngleInDegrees { get; set; }
        RGB Diffuse { get; set; }
        Vector Direction { get; }
        bool IsEnabled { get; set; }
        int Number { get; }
        RGB Specular { get; set; }

        string ToString();
    }

    public class Light : ILight
    {
        private double azimuthAngleInDegrees;
        private double altitudeAngleInDegrees;

        public Light(int lightNumber, RGB ambient, RGB diffuse,
            RGB specular, double azimuthAngleInDegrees, double altitudeAngleInDegrees)
        {
            IsEnabled = false;
            Number = lightNumber;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            AzimuthAngleInDegrees = azimuthAngleInDegrees;
            AltitudeAngleInDegrees = altitudeAngleInDegrees;

            CalculateDirection();
        }

        public bool IsEnabled { get; set; }

        public int Number { get; }

        public RGB Ambient { get; set; }

        public RGB Diffuse { get; set; }

        public RGB Specular { get; set; }

        public Vector Direction { get; private set; }

        public double AzimuthAngleInDegrees
        {
            get
            {
                return azimuthAngleInDegrees;
            }
            set
            {
                azimuthAngleInDegrees = value;
                CalculateDirection();
            }
        }

        public double AltitudeAngleInDegrees
        {
            get
            {
                return altitudeAngleInDegrees;
            }
            set
            {
                altitudeAngleInDegrees = value;
                CalculateDirection();
            }
        }

        public override string ToString()
        {
            return $"Light {Number}";
        }

        private void CalculateDirection()
        {
            Direction = Quaternion.Create(Vector.ZAxis, AzimuthAngleInDegrees).ApplyToVector(Vector.YAxis);
            Direction = Quaternion.Create(Direction.CrossProduct(Vector.ZAxis), AltitudeAngleInDegrees).ApplyToVector(Direction);
        }
    }
}
