using Colorado.Common.Colours;
using Colorado.Common.Extensions;
using Colorado.Geometry.Materials;
using Colorado.Geometry.MeshStructure;
using Colorado.Geometry.Structures.Geometry3D;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Rendering.Settings;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.Abstractions.Rendering
{
    public interface IGeometryRenderer
    {
        void DrawGeometryProvider(IGeometryProvider geometryProvider);
        void DrawGeometryProvider(IGeometryProvider geometryProvider, ITransform transform);
        void DrawGeometryProvider(IGeometryProvider geometryProvider, ITransform transform, PolygonMode polygonMode);

        void DrawGeometryProviderWithMaterial(IGeometryProvider geometryProvider);
        void DrawGeometryProviderWithMaterial(IGeometryProvider geometryProvider, ITransform transform);
        void DrawGeometryProviderWithMaterial(IGeometryProvider geometryProvider, ITransform transform, PolygonMode polygonMode);

        void DrawMesh(IMesh mesh);
        void DrawMesh(IMesh mesh, ITransform transform);
        void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        void DrawMeshWithMaterial(IMesh mesh);
        void DrawMeshWithMaterial(IMesh mesh, ITransform transform);
        void DrawMeshWithMaterial(IMesh mesh, ITransform transform, PolygonMode polygonMode);

        void DrawTriangle(Triangle triangle);
        void DrawTriangles(IEnumerable<Triangle> triangles);

        void DrawPoint(Point point, IRGB color, double size);

        void DrawCuboid(ICuboid cuboid, IRGB color);

        void DrawLines(IEnumerable<Line> lines, int width, IRGB color);
        void DrawLine(Line line, int width, IRGB color);

        void DrawCoordinateSystem(double axesLength, int width);

        void DrawRay(Ray ray, double length, IRGB color, int width);
    }

    public abstract class GeometryRenderer : IGeometryRenderer
    {
        #region Private fields

        private readonly IMaterialsManager _materialsManager;

        #endregion Private fields

        #region Constructor

        public GeometryRenderer(IMaterialsManager materialsManager)
        {
            _materialsManager = materialsManager;
        }

        #endregion Constructor

        #region Public logic

        public abstract void DrawTriangle(Triangle triangle);

        public void DrawTriangles(IEnumerable<Triangle> triangles)
        {
            foreach (Triangle triangle in triangles)
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

        public abstract void DrawPoint(Point point, IRGB color, double size);

        public void DrawCuboid(ICuboid cuboid, IRGB color) => DrawLines(cuboid.Lines, 1, color);

        public void DrawLines(IEnumerable<Line> lines, int width, IRGB color) => lines.ForEach(l => DrawLine(l, width, color));

        public abstract void DrawLine(Line line, int width, IRGB colour);

        public void DrawCoordinateSystem(double axisLength, int width)
        {
            DrawPoint(Point.Zero, RGB.BlackColor, width * 2);
            DrawLine(new Line(Point.Zero, Point.Zero + (Vector.XAxis * axisLength)), width, RGB.RedColor);
            DrawLine(new Line(Point.Zero, Point.Zero + (Vector.YAxis * axisLength)), width, RGB.GreenColor);
            DrawLine(new Line(Point.Zero, Point.Zero + (Vector.ZAxis * axisLength)), width, RGB.BlueColor);
        }

        public void DrawRay(Ray ray, double length, IRGB color, int width)
        {
            DrawLine(new Line(ray.Origin, ray.Origin + (ray.Direction * length)), width, color);
        }

        public void DrawGeometryProvider(IGeometryProvider geometryProvider)
            => DrawGeometryProvider(geometryProvider, Transform.Identity());

        public void DrawGeometryProvider(IGeometryProvider geometryProvider, ITransform transform)
            => DrawGeometryProvider(geometryProvider, transform, PolygonMode.Fill);

        public abstract void DrawGeometryProvider(IGeometryProvider geometryProvider, ITransform transform, PolygonMode polygonMode);

        public void DrawGeometryProviderWithMaterial(IGeometryProvider geometryProvider)
            => DrawGeometryProvider(geometryProvider, Transform.Identity());

        public void DrawGeometryProviderWithMaterial(IGeometryProvider geometryProvider, ITransform transform)
        => DrawGeometryProvider(geometryProvider, transform, PolygonMode.Fill);

        public void DrawGeometryProviderWithMaterial(IGeometryProvider geometryProvider, ITransform transform, PolygonMode polygonMode)
        {
            _materialsManager.SetMaterial(geometryProvider.Material);
            DrawGeometryProvider(geometryProvider, transform, polygonMode);
        }

        #endregion Public logic
    }
}
