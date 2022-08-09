using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Vector2D : IVector2D
    {
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }

        public double Y { get; }

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2D operator *(Vector2D vector, double scaleFactor)
        {
            return new Vector2D(vector.X * scaleFactor, vector.Y * scaleFactor);
        }

        public static Vector2D operator /(Vector2D vector, double scaleFactor)
        {
            return new Vector2D(vector.X / scaleFactor, vector.Y / scaleFactor);
        }
    }
}
