using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Abstractions.BoundingBoxStructures
{
    public interface IBoundingBox
    {
        IPoint Center { get; }
        double Diagonal { get; }
        bool IsEmpty { get; }
        IPoint MaxPoint { get; }
        IPoint MinPoint { get; }

        double SphereRadius { get; }

        IBoundingBox Add(IBoundingBox boundingBox);
        IBoundingBox ApplyTransform(ITransform transform);
        IBoundingBox Clone();
        void ResetToDefault();
    }
}
