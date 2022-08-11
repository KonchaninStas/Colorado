namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface IPoint2D
    {
        double X { get; }
        double Y { get; }

        IPoint2D Minus(IPoint2D anotherPoint);
    }
}
