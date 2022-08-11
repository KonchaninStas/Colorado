using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.MeshStructure;
using Colorado.Rendering.Controls.Abstractions.Rendering;
using Colorado.Rendering.Controls.Abstractions.Rendering.Settings;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Utilities;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering
{
    public class OpenGLGeometryRenderer : GeometryRenderer
    {
        public override void DrawTriangle(ITriangle triangle)
        {
            throw new NotImplementedException();
        }

        public override void DrawMesh(IMesh mesh, ITransform transform, PolygonMode polygonMode)
        {
            OpenGLRenderingWrapper.SetPolygonMode(polygonMode);
            using (new TransformApplier(transform))
            {
                OpenGLRenderingWrapper.DrawFastRenderingData(FastRenderingDataManager.Instance[mesh]);
            }
            OpenGLRenderingWrapper.SetPolygonMode(PolygonMode.Fill);
        }

        public override void DrawPoint(IPoint point, IRGB color, double size)
        {
            OpenGLRenderingWrapper.DrawPoint(point, color, size);
        }

        public override void DrawLine(ILine line, int width, IRGB color)
        {
            OpenGLRenderingWrapper.DrawLine(line, width, color);
        }
    }
}
