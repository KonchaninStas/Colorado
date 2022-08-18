using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.GeometryProviders;

namespace Colorado.Geometry.Structures.BaseStructures
{
    public interface IRenderableObject
    {
        bool IsModified { get; set; }
        IBoundingBox BoundingBox { get; }
        bool Visible { get; set; }
        IGeometryProvider GeometryProvider { get; }
    }

    public abstract class RenderableObject : IRenderableObject
    {
        public bool IsModified { get; set; }

        public bool Visible { get; set; }

        public abstract IBoundingBox BoundingBox { get; }

        public abstract IGeometryProvider GeometryProvider { get; }
    }
}
