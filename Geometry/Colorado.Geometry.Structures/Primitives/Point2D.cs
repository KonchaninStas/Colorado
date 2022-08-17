namespace Colorado.Geometry.Structures.Primitives
{
    public class Point2D
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

        #endregion Properties

        #region Operators

        public static Point2D operator -(Point2D leftPoint, Point2D rightPoint)
        {
            return new Point2D(leftPoint.X - rightPoint.X, leftPoint.Y - rightPoint.Y);
        }

        #endregion Operators
    }
}
