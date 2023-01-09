using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Settings;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Extensions
{
    internal static class PolygonModeExtensions
    {
        internal static OpenGLPolygonMode ToOpenGLPolygonMode(this PolygonMode polygonMode)
        {
            switch (polygonMode)
            {
                case PolygonMode.Point:
                    return OpenGLPolygonMode.Point;
                case PolygonMode.Line:
                    return OpenGLPolygonMode.Line;
                case PolygonMode.Fill:
                    return OpenGLPolygonMode.Fill;
                default:
                    throw new Exception();
            }
        }
    }
}
