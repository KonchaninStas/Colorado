using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Structures;
using Colorado.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl
{
    public class OpenGLRenderingControl : IRenderingControl
    {
        private Context _renderingContext;

        public void DrawScene()
        {
            throw new NotImplementedException();
        }

        public void Initialize(IntPtr windowHandle)
        {
            try
            {
                _renderingContext = new Context(windowHandle, 32, 32, 8);
                OpenGLViewportWrapper.ClearColor(BackgroundColor);
                OpenGLWrapper.SetShadingMode(ShadingModel.Smooth);
                return true;
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError(ex);
            }
        }

        public void Dispose()
        {
            _renderingContext.Dispose();
        }
    }
}
