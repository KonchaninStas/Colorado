using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Triangle : ITriangle
    {
        public Triangle(IPoint firstVertex, IPoint secondVertex, IPoint thirdPoint)
            : this(firstVertex, secondVertex, thirdPoint, GetNormalVector(firstVertex, secondVertex, thirdPoint))
        {
        }

        public Triangle(IPoint firstVertex, IPoint secondVertex, IPoint thirdVertex, IVector normalVector)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;
            Normal = normalVector;
            Color = RGB.GetRandomColour();
        }

        public IPoint FirstVertex { get; }

        public IPoint SecondVertex { get; }

        public IPoint ThirdVertex { get; }

        public IVector Normal { get; }

        public IRGB Color { get; }

        public static ITriangle GetRandomTriangle()
        {
            return new Triangle(Point.GetRandomPoint(), Point.GetRandomPoint(), Point.GetRandomPoint());
        }

        private static IVector GetNormalVector(IPoint firstVertex, IPoint secondVertex, IPoint thirdVertex)
        {
            IVector firstVector = new Vector(secondVertex, firstVertex);
            IVector secondVector = new Vector(thirdVertex, secondVertex);

            IVector normal = firstVector.CrossProduct(secondVector);
            return normal.UnitVector;
        }
    }
}
