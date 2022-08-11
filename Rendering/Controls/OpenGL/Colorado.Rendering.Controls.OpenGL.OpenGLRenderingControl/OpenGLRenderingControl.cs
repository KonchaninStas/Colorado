using Colorado.ModelStructure;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.View;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Lighting;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene;
using Colorado.Rendering.Controls.OpenGL.RenderingControl.Structures;
using Colorado.Services.Logger;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl
{
    public class OpenGLRenderingControl : Abstractions.RenderingControl
    {
        private Context _renderingContext;

        public OpenGLRenderingControl(IModel model, OpenGLLightsManager lightsManager,
            OpenGLViewport viewport, OpenGLGeometryRenderer openGLGeometryRenderer)
            : base(model, lightsManager, viewport, openGLGeometryRenderer)
        {

        }

        public override void Initialize(IntPtr windowHandle)
        {
            try
            {
                _renderingContext = new Context(windowHandle, 32, 32, 8);
                OpenGLSceneWrapper.ClearColor(BackgroundColor);
                OpenGLSceneWrapper.SetShadingMode(ShadingModel.Smooth);
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError(ex);
            }
        }

        public override void BeforeDrawScene()
        {
            _renderingContext.MakeCurrent();
            OpenGLGeneralWrapper.EnableCapability(OpenGLCapability.DepthTest);
            OpenGLGeneralWrapper.EnableCapability(OpenGLCapability.PointSmooth);
            OpenGLGeneralWrapper.EnableCapability(OpenGLCapability.NormalizeNormals);

            OpenGLSceneWrapper.ClearColor(BackgroundColor);
            OpenGLSceneWrapper.ClearDepthBufferValue();
            OpenGLSceneWrapper.ClearBuffers(OpenGLBufferType.Color, OpenGLBufferType.Depth);

            OpenGLGeneralWrapper.EnableCapability(OpenGLCapability.CullFace);
            OpenGLGeneralWrapper.SetFrontFaceVerticesOrder(VerticesOrder.Clockwise);
            OpenGLGeneralWrapper.CullFace(FaceSide.Back);

            Viewport.Apply();
        }

        public override void EndDrawScene()
        {
            OpenGLSceneWrapper.Flush();
            _renderingContext.SwapBuffers();
        }

        #region IDisposable

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _renderingContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _isDisposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RenderingControl()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public override void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
