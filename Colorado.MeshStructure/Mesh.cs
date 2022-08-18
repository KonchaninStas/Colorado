using Colorado.Geometry.Structures.BaseStructures;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Materials;
using System.Collections.Generic;

namespace Colorado.MeshStructure
{
    public interface IMesh : IRenderableObject
    {
        IList<Triangle> Triangles { get; }
        IMaterial Material { get; }
    }

    public sealed class Mesh : RenderableObject, IMesh
    {
        #region Constructor

        public Mesh(IList<Triangle> triangles) : this(triangles, Rendering.Materials.Material.Default)
        {
        }

        public Mesh(IList<Triangle> triangles, IMaterial material)
        {
            Triangles = triangles;
            BoundingBox = new BoundingBox(triangles);
            Material = material;
            GeometryProvider = new TrianglesGeometryProvider(Triangles, Material);
        }

        #endregion Constructor

        #region Properties

        public IList<Triangle> Triangles { get; }

        public IMaterial Material { get; }

        public override IBoundingBox BoundingBox { get; }

        public override IGeometryProvider GeometryProvider { get; }

        #endregion Properties
    }
}
