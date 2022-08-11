using Colorado.Common.Colours;
using Colorado.Common.Extensions;
using Colorado.Geometry.Abstractions.Geometry3D;
using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using Colorado.MeshStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering.Settings;
using Colorado.Rendering.Materials;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.Abstractions.Rendering
{
    public interface IGeometryRenderer
    {
        void DrawMesh(IMesh mesh);
        void DrawMesh(IMesh mesh, ITransform transform);
        void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        void DrawMeshWithMaterial(IMesh mesh);
        void DrawMeshWithMaterial(IMesh mesh, ITransform transform);
        void DrawMeshWithMaterial(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        void DrawTriangle(ITriangle triangle);
        void DrawTriangles(IEnumerable<ITriangle> triangles);

        void DrawPoint(IPoint point, IRGB color, double size);

        void DrawCuboid(ICuboid cuboid, IRGB color);

        void DrawLines(IEnumerable<ILine> lines, int width, IRGB colour);
        void DrawLine(ILine line, int width, IRGB colour);

        void DrawCoordinateSystem(double axesLength);
    }

    public abstract class GeometryRenderer : IGeometryRenderer
    {
        private readonly IMaterialsManager _materialsManager;

        public GeometryRenderer(IMaterialsManager materialsManager)
        {
            _materialsManager = materialsManager;
        }

        public abstract void DrawTriangle(ITriangle triangle);

        public void DrawTriangles(IEnumerable<ITriangle> triangles)
        {
            foreach (ITriangle triangle in triangles)
            {
                DrawTriangle(triangle);
            }
        }

        public void DrawMesh(IMesh mesh) => DrawMesh(mesh, Transform.Identity(), PolygonMode.Fill);

        public void DrawMesh(IMesh mesh, ITransform transform) => DrawMesh(mesh, transform, PolygonMode.Fill);

        public abstract void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        public void DrawMeshWithMaterial(IMesh mesh) => DrawMeshWithMaterial(mesh, Transform.Identity(), PolygonMode.Fill);

        public void DrawMeshWithMaterial(IMesh mesh, ITransform transform) => DrawMeshWithMaterial(mesh, transform, PolygonMode.Fill);

        public void DrawMeshWithMaterial(IMesh mesh, ITransform transform, PolygonMode polygonMode)
        {
            _materialsManager.SetMaterial(mesh.Material);
            DrawMesh(mesh, transform, polygonMode);
        }

        public abstract void DrawPoint(IPoint point, IRGB color, double size);

        public void DrawCuboid(ICuboid cuboid, IRGB color) => DrawLines(cuboid.Lines, 1, color);

        public void DrawLines(IEnumerable<ILine> lines, int width, IRGB color) => lines.ForEach(l => DrawLine(l, width, color));

        public abstract void DrawLine(ILine line, int width, IRGB colour);

        public void DrawCoordinateSystem(double axisLength)
        {
            DrawPoint(Point.Zero, RGB.BlackColor, 20);
            DrawLine(new Line(Point.Zero, Point.Zero.Plus(Vector.XAxis.Multiply(axisLength))), 10, RGB.RedColor);
            DrawLine(new Line(Point.Zero, Point.Zero.Plus(Vector.YAxis.Multiply(axisLength))), 10, RGB.GreenColor);
            DrawLine(new Line(Point.Zero, Point.Zero.Plus(Vector.ZAxis.Multiply(axisLength))), 10, RGB.BlueColor);
        }
    }
}
