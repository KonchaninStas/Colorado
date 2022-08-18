﻿using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering
{
    public interface IFastRenderingData
    {
        double[] VerticesValuesArray { get; }
        double[] NormalsValuesArray { get; }
        int VerticesCount { get; }
        byte[] VerticesColorsValuesArray { get; }

        Primitive Primitive { get; }
    }
}
