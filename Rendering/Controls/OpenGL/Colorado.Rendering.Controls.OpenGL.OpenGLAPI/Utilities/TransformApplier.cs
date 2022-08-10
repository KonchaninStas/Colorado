using Colorado.Geometry.Abstractions.Math;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Utilities
{
    public class TransformApplier : IDisposable
    {
        public TransformApplier(ITransform transform)
        {
            OpenGLMatrixOperationWrapper.PushMatrix();
            OpenGLMatrixOperationWrapper.MultiplyWithCurrentMatrix(transform);
        }

        public void Dispose()
        {
            OpenGLMatrixOperationWrapper.PopMatrix();
        }
    }
}
