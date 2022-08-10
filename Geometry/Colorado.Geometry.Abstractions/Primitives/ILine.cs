using Colorado.Geometry.Abstractions.Math;

namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface ILine
    {
        IPoint End { get; }
        IPoint Start { get; }

        ILine ApplyTramsform(ITransform transform);
    }
}
