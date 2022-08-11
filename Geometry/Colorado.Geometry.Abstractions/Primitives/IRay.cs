using Colorado.Geometry.Abstractions.Math;

namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface IRay
    {
        IVector Direction { get; }
        IPoint Origin { get; }

        IRay ApplyTramsform(ITransform transform);
    }
}
