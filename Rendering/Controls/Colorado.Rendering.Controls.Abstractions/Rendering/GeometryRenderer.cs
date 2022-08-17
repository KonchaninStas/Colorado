﻿using Colorado.Common.Colours;
using Colorado.Common.Extensions;
using Colorado.Geometry.Structures.Geometry3D;
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
        private readonly IMaterialsManager _materialsManager;

        public GeometryRenderer(IMaterialsManager materialsManager)
        {
            _materialsManager = materialsManager;
        }

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
    }
}
