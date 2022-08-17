using System;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Point
    {
        #region Private fields

        private static readonly Random _random = new Random();

        #endregion Private fields

        #region Constructor

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion Constructor

        #region Properties

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public static Point Zero => new Point(default(double), default(double), default(double));

        public Point Inverse => this * -1;

        #endregion Properties

        #region Public logic

        public double DistanceTo(Point secondPoint)
        {
            return System.Math.Abs((this - secondPoint).Length);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);
        }

        public static Point GetRandomPoint()
        {
            return new Point(_random.Next(-100, 100), _random.Next(-100, 100), _random.Next(-100, 100));
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}, Z = {Z}";
        }

        #endregion Public logic

        #region Operators

        public static Point operator +(Point point, Vector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Point operator +(Point leftPoint, Point rightPoint)
        {
            return new Point(leftPoint.X + rightPoint.X, leftPoint.Y + rightPoint.Y, leftPoint.Z + rightPoint.Z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
        }

        public static Vector operator -(Point leftPoint, Point rightPoint)
        {
            return new Vector(leftPoint.X - rightPoint.X, leftPoint.Y - rightPoint.Y, leftPoint.Z - rightPoint.Z);
        }

        public static Point operator *(Point point, double scaleFactor)
        {
            return new Point(point.X * scaleFactor, point.Y * scaleFactor, point.Z * scaleFactor);
        }

        public static Point operator /(Point point, double scaleFactor)
        {
            return new Point(point.X / scaleFactor, point.Y / scaleFactor, point.Z / scaleFactor);
        }

        #endregion Operators
    }
}
