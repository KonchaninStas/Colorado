using Colorado.Geometry.Abstractions.BoundingBoxStructures;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using System.Collections.Generic;

namespace Colorado.MeshStructure
{
    public interface IMesh
    {
        IBoundingBox BoundingBox { get; }
        IList<ITriangle> Triangles { get; }
    }

    public class Mesh : IMesh
    {
        public Mesh(IList<ITriangle> triangles)
        {
            Triangles = triangles;
            BoundingBox = new BoundingBox(triangles);

        }

        public IList<ITriangle> Triangles { get; }

        public IBoundingBox BoundingBox { get; }
    }
}
