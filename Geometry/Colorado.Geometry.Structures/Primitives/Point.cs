using Colorado.Geometry.Abstractions.Primitives;
using System;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Point : IPoint
    {
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public static IPoint ZeroPoint => new Point(default(double), default(double), default(double));

        public static Point operator +(Point point, IVector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public double DistanceTo(IPoint secondPoint)
        {
            return System.Math.Abs(Minus(secondPoint).Length);
        }

        public IVector Minus(IPoint right)
        {
            return new Vector(X - right.X, Y - right.Y, Z - right.Z);
        }

        public IPoint Plus(IVector vector)
        {
            return new Point(X + vector.X, Y + vector.Y, Z + vector.Z);
        }

        public IVector ToVector()
        {
            return new Vector(X, Y, Z);
        }
    }
}
