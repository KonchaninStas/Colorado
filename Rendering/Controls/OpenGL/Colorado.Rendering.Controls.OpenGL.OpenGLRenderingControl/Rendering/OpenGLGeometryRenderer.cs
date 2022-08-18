using Colorado.Common.Colours;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using Colorado.MeshStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.Abstractions.Rendering.Settings;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Utilities;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Managers;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering
{
    public class OpenGLGeometryRenderer : GeometryRenderer
    {
        public OpenGLGeometryRenderer(OpenGLMaterialsManager openGLMaterialsManager) : base(openGLMaterialsManager)
        {

        }

        public override void DrawTriangle(Triangle triangle)
        {
            throw new NotImplementedException();
        }

        public override void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode)
        {
            OpenGLRenderingWrapper.SetPolygonMode(polygonMode);
            using (new TransformApplier(transform))
            {
                OpenGLRenderingWrapper.DrawFastRenderingData(FastRenderingDataManager.Instance[mesh.GeometryProvider]);
            }
            OpenGLRenderingWrapper.SetPolygonMode(PolygonMode.Fill);
        }

        public override void DrawPoint(Point point, IRGB color, double size)
        {
            OpenGLRenderingWrapper.DrawPoint(point, color, size);
        }

        public override void DrawLine(Line line, int width, IRGB color)
        {
            OpenGLRenderingWrapper.DrawLine(line, width, color);
        }

        public override void DrawGeometryProvider(IGeometryProvider geometryProvider, ITransform transform, PolygonMode polygonMode)
        {
            //OpenGLRenderingWrapper.SetPolygonMode(polygonMode);
            using (new TransformApplier(transform))
            {
                OpenGLRenderingWrapper.DrawFastRenderingData(FastRenderingDataManager.Instance[geometryProvider]);
            }
            //OpenGLRenderingWrapper.SetPolygonMode(PolygonMode.Fill);
        }
    }
}
