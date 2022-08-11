namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface IVector
    {
        double X { get; }

        double Y { get; }

        double Z { get; }

        double Length { get; }

        bool IsZero { get; }

        IVector UnitVector { get; }

        float[] FloatArray { get; }

        IVector CrossProduct(IVector anotherVector);


        IVector Multiply(double scaleFactor);

        IVector GetInversed();

        IVector Plus(IVector vector);
        double DotProduct(IVector anotherVector);
        string ToString();
        double CosToVector(IVector anotherVector);
        double AngleToVectorInRadians(IVector anotherVector);
    }
}
