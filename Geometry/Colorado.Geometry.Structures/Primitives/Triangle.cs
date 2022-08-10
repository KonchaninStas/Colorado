using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Triangle : ITriangle
    {
        public Triangle(IPoint firstVertex, IPoint secondVertex, IPoint thirdPoint)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdPoint;
            Normal = GetNormalVector();
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

        private IVector GetNormalVector()
        {
            IVector firstVector = new Vector(SecondVertex, FirstVertex);
            IVector secondVector = new Vector(ThirdVertex, SecondVertex);

            IVector normal = firstVector.CrossProduct(secondVector);
            return normal.UnitVector;
        }
    }
}
