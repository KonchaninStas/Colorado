using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Abstractions.Math
{
    public interface IQuaternion
    {
        double X { get; }
        double Y { get; }
        double Z { get; }
        double W { get; }

        double AngleInRadians { get; }
        IVector Axis { get; }
        bool IsIdentity { get; }

        IVector ApplyToVector(IVector vector);
        IEulerAngles GetEulerAngles();
        IQuaternion GetInversed();
        IQuaternion Multiply(IQuaternion rhs);
    }
}
