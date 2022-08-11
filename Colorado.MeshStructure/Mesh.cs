using Colorado.Geometry.Abstractions.BoundingBoxStructures;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Rendering.Materials;
using System.Collections.Generic;

namespace Colorado.MeshStructure
{
    public interface IMesh
    {
        IBoundingBox BoundingBox { get; }
        IList<ITriangle> Triangles { get; }
        IMaterial Material { get; }
    }

    public class Mesh : IMesh
    {
        public Mesh(IList<ITriangle> triangles) : this(triangles, Rendering.Materials.Material.Default)
        {
        }

        public Mesh(IList<ITriangle> triangles, IMaterial material)
        {
            Triangles = triangles;
            BoundingBox = new BoundingBox(triangles);
            Material = material;
        }

        public IList<ITriangle> Triangles { get; }

        public IBoundingBox BoundingBox { get; }

        public IMaterial Material { get; }
    }
}
