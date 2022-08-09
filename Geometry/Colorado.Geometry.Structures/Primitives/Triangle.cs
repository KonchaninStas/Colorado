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
        }

        public IPoint FirstVertex { get; }

        public IPoint SecondVertex { get; }

        public IPoint ThirdVertex { get; }

        public static ITriangle GetRandomTriangle()
        {
            return new Triangle(Point.GetRandomPoint(), Point.GetRandomPoint(), Point.GetRandomPoint());
        }
    }
}
