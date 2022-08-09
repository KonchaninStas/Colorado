namespace Colorado.Geometry.Abstractions.Primitives
{
    public interface ITriangle
    {
        IPoint FirstVertex { get; }

        IPoint SecondVertex { get; }

        IPoint ThirdVertex { get; }
    }
}
