using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Materials;
using System.Collections.Generic;

namespace Colorado.Geometry.Structures.GeometryProviders
{
    public interface ITrianglesGeometryProvider : IGeometryProvider
    {
        IList<Triangle> Triangles { get; }
    }

    public sealed class TrianglesGeometryProvider : GeometryProvider, ITrianglesGeometryProvider
    {
        public TrianglesGeometryProvider(IList<Triangle> triangles, IMaterial material) : base(material)
        {
            Triangles = triangles;
        }

        public IList<Triangle> Triangles { get; }
    }
}
