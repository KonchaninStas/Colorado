﻿using Colorado.Geometry.Structures.Math;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Utilities
{
    public class TransformApplier : IDisposable
    {
        public TransformApplier(ITransform transform)
        {
            OpenGLMatrixOperationWrapper.PushMatrix();
            OpenGLMatrixOperationWrapper.MultiplyWithCurrentMatrix(transform.Multiply(Transform.CreateTranslation(
                new Geometry.Structures.Primitives.Vector(0,0,0))));
        }

        public void Dispose()
        {
            OpenGLMatrixOperationWrapper.PopMatrix();
        }
    }
}
