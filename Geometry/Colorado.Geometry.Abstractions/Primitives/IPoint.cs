namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface IPoint
    {
        double X { get; }

        double Y { get; }

        double Z { get; }
        IPoint Inverse { get; }

        double DistanceTo(IPoint secondPoint);

        IVector Minus(IPoint right);

        IPoint Minus(IVector right);

        IPoint Plus(IVector vector);

        IPoint Plus(IPoint vector);

        IVector ToVector();
        IPoint Divide(double number);
        string ToString();
        IPoint Multiply(double scaleFactor);
    }
}
