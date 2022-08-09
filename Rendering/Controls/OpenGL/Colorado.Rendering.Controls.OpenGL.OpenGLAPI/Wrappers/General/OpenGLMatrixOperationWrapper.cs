using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General
{
    public static class OpenGLMatrixOperationWrapper
    {
        public static void SetActiveMatrixType(MatrixType matrixType)
        {
            OpenGLMatrixOperationAPI.MatrixMode((int)matrixType);
        }

        public static void MakeActiveMatrixIdentity()
        {
            OpenGLMatrixOperationAPI.LoadIdentity();
        }

        public static void TranslateCurrentMatrix(IPoint point)
        {
            OpenGLMatrixOperationAPI.Translated(point.X, point.Y, point.Z);
        }

        public static void TranslateCurrentMatrix(IVector vector)
        {
            OpenGLMatrixOperationAPI.Translated(vector.X, vector.Y, vector.Z);
        }

        public static void LoadMatrix(ITransform transform)
        {
            OpenGLMatrixOperationAPI.LoadMatrixd(transform.Array);
        }
    }
}
