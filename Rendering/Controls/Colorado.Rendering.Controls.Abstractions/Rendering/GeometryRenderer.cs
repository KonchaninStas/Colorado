using Colorado.Common.Colours;
using Colorado.Common.Extensions;
using Colorado.Geometry.Abstractions.Geometry3D;
using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Math;
using Colorado.MeshStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering.Settings;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.Abstractions.Rendering
{
    public interface IGeometryRenderer
    {
        void DrawMesh(IMesh mesh);
        void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        void DrawTriangle(ITriangle triangle);
        void DrawTriangles(IEnumerable<ITriangle> triangles);

        void DrawPoint(IPoint point, IRGB color, double size);

        void DrawCuboid(ICuboid cuboid, IRGB color);

        void DrawLines(IEnumerable<ILine> lines, int width, IRGB colour);
        void DrawLine(ILine line, int width, IRGB colour);
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

        public void DrawMesh(IMesh mesh) => DrawMesh(mesh, Transform.Identity(), PolygonMode.Fill);

        public abstract void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        public abstract void DrawPoint(IPoint point, IRGB color, double size);

        public void DrawCuboid(ICuboid cuboid, IRGB color) => DrawLines(cuboid.Lines, 1, color);

        public void DrawLines(IEnumerable<ILine> lines, int width, IRGB color) => lines.ForEach(l => DrawLine(l, width, color));

        public abstract void DrawLine(ILine line, int width, IRGB colour);
    }
}
