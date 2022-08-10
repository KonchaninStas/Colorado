using Colorado.Common.Colours;

namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface ITriangle
    {
        IPoint FirstVertex { get; }

        IPoint SecondVertex { get; }

        IPoint ThirdVertex { get; }

        IVector Normal { get; }

        IRGB Color { get; }
    }
}
