using Colorado.Common.Logging;
using Colorado.Common.WindowsLibrariesWrappers.Gdi32;
using Colorado.Common.WindowsLibrariesWrappers.Gdi32.Structures;
using Colorado.Common.WindowsLibrariesWrappers.Kernel32;
using Colorado.Common.WindowsLibrariesWrappers.User32;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Extensions;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using System;

namespace Colorado.Rendering.Controls.OpenGL.RenderingControl.Structures
{
    public class Context : IDisposable
    {
        private readonly IntPtr openGLControlHandle;
        private IntPtr deviceContext;
        private IntPtr renderingContext;
        private IntPtr windowHandle;

        public Context(IntPtr hwnd, byte color, byte depth, byte stencil)
        {
            Boolean initialLoad = false;
            windowHandle = hwnd;
            if (openGLControlHandle == IntPtr.Zero)
            {
                openGLControlHandle = Kernel32LibraryWrapper.Instance.LoadLibrary("OPENGL32.DLL");
                initialLoad = true;
            }
            deviceContext = User32LibraryWrapper.Instance.GetDeviceContext(windowHandle);
            var pfd = new PixelFormatDescriptor()
            {
                Size = 40,
                Version = 1,
                dwFlags = (uint)(PixelBufferFlags.DRAW_TO_WINDOW | PixelBufferFlags.SUPPORT_OPENGL | PixelBufferFlags.DOUBLEBUFFER),
                PixelType = (byte)PixelTypes.TYPE_RGBA,
                ColorBits = color,
                AlphaBits = 8,
                DepthBits = depth,
                StencilBits = stencil
            };

            int nPixelFormat = Gdi32LibraryWrapper.Instance.ChoosePixelFormat(deviceContext, pfd);
            if (nPixelFormat == 0)
            {
                Logger.Instance.LogDebug("ChoosePixelFormat failed.");
                return;
            }

            Gdi32LibraryWrapper.Instance.SetPixelFormat(deviceContext, nPixelFormat, pfd);
            renderingContext = OpenGLWglWrapper.CreateContext(deviceContext);
            MakeCurrent();
            if (initialLoad)
            {
                OpenGLExtensionsWrapper.LoadExtensions();
            }
        }

        public IntPtr DeviceContext
        {
            get
            {
                return deviceContext;
            }
        }

        public IntPtr RenderingContext
        {
            get
            {
                return renderingContext;
            }
        }

        public void MakeCurrent()
        {
            OpenGLWglWrapper.SetCurrentRenderingContext(deviceContext, renderingContext);
        }

        public void SwapBuffers()
        {
            Gdi32LibraryWrapper.Instance.SwapBuffers(deviceContext);
        }

        #region IDisposable

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (renderingContext != IntPtr.Zero)
                {
                    OpenGLWglWrapper.DeleteOpenGLRenderingContext(renderingContext);
                    renderingContext = IntPtr.Zero;
                }
                if (deviceContext != IntPtr.Zero)
                {
                    User32LibraryWrapper.Instance.ReleaseDeviceContext(windowHandle, deviceContext);
                    deviceContext = IntPtr.Zero;
                }
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
