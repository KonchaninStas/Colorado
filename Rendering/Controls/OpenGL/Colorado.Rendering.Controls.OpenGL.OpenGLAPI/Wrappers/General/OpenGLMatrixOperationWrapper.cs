using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Math;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General
{
    public static class OpenGLMatrixOperationWrapper
    {
        const int modelViewMatrixLength = 16;

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

        private static double[] GetParameterValues(ViewMatrixArrayType viewMatrixArrayType)
        {
            var assignedParameterValues = new double[16];
            OpenGLMatrixOperationAPI.GetDoublev((int)viewMatrixArrayType, assignedParameterValues);

            return assignedParameterValues;
        }

        public static int[] GetParameterValuesArray(OpenGLCapability capability, int valuesArraySize)
        {
            var size = new int[valuesArraySize];
            OpenGLMatrixOperationAPI.GetParameterValuesArray((uint)capability, size);
            return size;
        }

        public static ITransform GetModelViewMatrix()
        {
            return new Transform(GetParameterValues(ViewMatrixArrayType.ModelView));
        }

        public static ITransform GetProjectionMatrix()
        {
            return new Transform(GetParameterValues(ViewMatrixArrayType.Projection));
        }

        public static ITransform GetMatrix(MatrixType matrixType)
        {
            var viewMatrix = new double[modelViewMatrixLength];
            OpenGLMatrixOperationAPI.GetDoublev((int)matrixType, viewMatrix);
            return new Transform(viewMatrix);
        }
        public static void PushMatrix()
        {
            OpenGLMatrixOperationAPI.PushMatrix();
        }

        public static void PopMatrix()
        {
            OpenGLMatrixOperationAPI.PopMatrix();
        }

        public static void MultiplyWithCurrentMatrix(ITransform transform)
        {
            OpenGLMatrixOperationAPI.MultMatrixd(transform.Array);
        }
    }
}
