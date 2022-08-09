using Colorado.Common.Extensions;
using Colorado.Geometry.Abstractions.Primitives;
using System;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Vector : IVector
    {
        public Vector(IPoint startPoint, IPoint endPoint)
            : this(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y, endPoint.Z - startPoint.Z) { }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Length = CalculateLength();
            UnitVector = new Vector(X / Length, Y / Length, Z / Length);
            IsZero = X.IsZero() && Y.IsZero() && Z.IsZero();

            FloatArray = new float[] { (float)X, (float)Y, (float)Z, 0 };
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Length { get; }

        public IVector UnitVector { get; }

        public bool IsZero { get; }

        public float[] FloatArray { get; }

        public static IVector ZeroVector => new Vector(0, 0, 0);

        public static IVector XAxis => new Vector(1, 0, 0);

        public static IVector YAxis => new Vector(0, 1, 0);

        public static IVector ZAxis => new Vector(0, 0, 1);

        public IVector CrossProduct(IVector anotherVector)
        {
            double x = Y * anotherVector.Z - anotherVector.Y * Z;
            double y = (X * anotherVector.Z - anotherVector.X * Z) * -1;
            double z = X * anotherVector.Y - anotherVector.X * Y;

            return new Vector(x, y, z);
        }

        public double DotProduct(IVector anotherVector)
        {
            return X * anotherVector.X + Y * anotherVector.Y + Z * anotherVector.Z;
        }

        public static IVector operator *(Vector vector, double scaleFactor)
        {
            return new Vector(vector.X * scaleFactor, vector.Y * scaleFactor, vector.Z * scaleFactor);
        }

        private double CalculateLength()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public IVector Multiply(double scaleFactor)
        {
            return new Vector(X * scaleFactor, Y * scaleFactor, Z * scaleFactor);
        }

        public IVector GetInversed()
        {
            return new Vector(X * -1, Y * -1, Z * -1);
        }

        public IVector Plus(IVector vector)
        {
            return new Vector(X + vector.X, Y + vector.Y, Z + vector.Z);
        }
    }
}
