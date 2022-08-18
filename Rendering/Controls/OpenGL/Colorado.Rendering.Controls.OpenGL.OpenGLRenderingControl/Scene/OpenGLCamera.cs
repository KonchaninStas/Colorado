using Colorado.Documents;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene
{
    public class OpenGLCamera : Camera
    {
        public OpenGLCamera(IDocumentsManager documentsManager) : base(documentsManager)
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
