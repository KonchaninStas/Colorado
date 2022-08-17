using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Materials;
using System.Collections.Generic;

namespace Colorado.MeshStructure
{
    public interface IMesh
    {
        IBoundingBox BoundingBox { get; }
        IList<Triangle> Triangles { get; }
        IMaterial Material { get; }
    }

    public class Mesh : IMesh
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
        }

        #endregion Constructor

        #region Properties

        public IList<Triangle> Triangles { get; }

        public IBoundingBox BoundingBox { get; }

        public IMaterial Material { get; }

        #endregion Properties
    }
}
