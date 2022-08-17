using Colorado.Common.Colours;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Triangle
    {
        #region Constructor

        public Triangle(Point firstVertex, Point secondVertex, Point thirdPoint)
            : this(firstVertex, secondVertex, thirdPoint, GetNormalVector(firstVertex, secondVertex, thirdPoint))
        {
        }

        public Triangle(Point firstVertex, Point secondVertex, Point thirdVertex, Vector normalVector)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;
            Normal = normalVector;
            Color = RGB.GetRandomColour();
        }

        #endregion Constructor

        #region Properties

        public Point FirstVertex { get; }

        public Point SecondVertex { get; }

        public Point ThirdVertex { get; }

        public Vector Normal { get; }

        public IRGB Color { get; }

        #endregion Properties

        #region Public logic

        public static Triangle GetRandomTriangle()
        {
            return new Triangle(Point.GetRandomPoint(), Point.GetRandomPoint(), Point.GetRandomPoint());
        }

        #endregion Public logic

        #region Private logic

        private static Vector GetNormalVector(Point firstVertex, Point secondVertex, Point thirdVertex)
        {
            Vector firstVector = new Vector(secondVertex, firstVertex);
            Vector secondVector = new Vector(thirdVertex, secondVertex);

            Vector normal = firstVector.CrossProduct(secondVector);
            return normal.UnitVector;
        }

        #endregion Private logic
    }
}
