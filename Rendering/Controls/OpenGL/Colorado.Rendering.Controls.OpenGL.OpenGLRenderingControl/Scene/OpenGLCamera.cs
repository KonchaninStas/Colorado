using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using Colorado.Rendering.Utils;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene
{
    public class OpenGLCamera : Camera
    {
        public OpenGLCamera(ITotalBoundingBoxProvider totalBoundingBoxProvider)
            : base(totalBoundingBoxProvider)
        {

        }

        internal void ApplyProjectionMatrix()
        {
            OpenGLMatrixOperationWrapper.SetActiveMatrixType(MatrixType.Projection);
            OpenGLMatrixOperationWrapper.MakeActiveMatrixIdentity();
        }

        internal void ApplyModelViewMatrix()
        {
            OpenGLMatrixOperationWrapper.SetActiveMatrixType(MatrixType.ModelView);
            OpenGLMatrixOperationWrapper.MakeActiveMatrixIdentity();

            OpenGLMatrixOperationWrapper.LoadMatrix(GetViewMatrix());
            OpenGLMatrixOperationWrapper.TranslateCurrentMatrix(Position);
        }
    }
}
