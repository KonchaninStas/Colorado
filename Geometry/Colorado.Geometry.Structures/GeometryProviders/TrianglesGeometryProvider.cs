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
        #region Constructor

        public TrianglesGeometryProvider(IList<Triangle> triangles, IMaterial material) : base(material)
        {
            Triangles = triangles;
        }

        #endregion Constructor

        #region Properties

        public IList<Triangle> Triangles { get; }

        #endregion Properties
    }
}
