using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Point2D : IPoint2D
    {
        #region Constructors

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion Constructors

        #region Properties

        public double X { get; }

        public double Y { get; }

        public static Point2D Zero => new Point2D(0, 0);

        public IPoint2D Minus(IPoint2D anotherPoint)
        {
            return new Point2D(X - anotherPoint.X, Y - anotherPoint.Y);
        }

        #endregion Properties
    }
}
