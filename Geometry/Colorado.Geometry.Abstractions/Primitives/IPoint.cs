namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface IPoint
    {
        double X { get; }

        double Y { get; }

        double Z { get; }

        double DistanceTo(IPoint secondPoint);

        IVector Minus(IPoint right);

        IPoint Plus(IVector vector);

        IVector ToVector();
    }
}
