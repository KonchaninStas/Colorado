using Colorado.Geometry.Abstractions.Primitives;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.Abstractions.Rendering
{
    public interface IGeometryRenderer
    {
        void DrawTriangle(ITriangle triangle);
        void DrawTriangles(IEnumerable<ITriangle> triangles);
    }

    public abstract class GeometryRenderer : IGeometryRenderer
    {
        public abstract void DrawTriangle(ITriangle triangle);

        public void DrawTriangles(IEnumerable<ITriangle> triangles)
        {
            foreach (ITriangle triangle in triangles)
            {
                DrawTriangle(triangle);
            }
        }
    }
}
