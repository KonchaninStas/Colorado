using Colorado.Common.Logging;
using Colorado.Common.WindowsLibrariesWrappers;
using Colorado.Common.WindowsLibrariesWrappers.Gdi32.Structures;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Extensions;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;
using System;

namespace Colorado.Rendering.Controls.OpenGL.RenderingControl.Structures
{
    public class Context : IDisposable
    {
        private readonly IWindowsLibrariesWrapper _windowsLibrariesWrapper;
        private readonly ILogger _logger;
        private readonly IntPtr openGLControlHandle;
        private IntPtr deviceContext;
        private IntPtr renderingContext;
        private IntPtr windowHandle;

        public Context(IntPtr hwnd, byte color, byte depth, byte stencil,
            IWindowsLibrariesWrapper windowsLibrariesWrapper, ILogger logger)
        {
            _windowsLibrariesWrapper = windowsLibrariesWrapper;
            _logger = logger;
            bool initialLoad = false;
            windowHandle = hwnd;
            if (openGLControlHandle == IntPtr.Zero)
            {
                openGLControlHandle = _windowsLibrariesWrapper.Kernel32LibraryWrapper.LoadLibrary("OPENGL32.DLL");
                initialLoad = true;
            }
            deviceContext = _windowsLibrariesWrapper.User32LibraryWrapper.GetDeviceContext(windowHandle);
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

            int nPixelFormat = _windowsLibrariesWrapper.Gdi32LibraryWrapper.ChoosePixelFormat(deviceContext, pfd);
            if (nPixelFormat == 0)
            {
                _logger.LogDebug("ChoosePixelFormat failed.");
                return;
            }

            _windowsLibrariesWrapper.Gdi32LibraryWrapper.SetPixelFormat(deviceContext, nPixelFormat, pfd);
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
            _windowsLibrariesWrapper.Gdi32LibraryWrapper.SwapBuffers(deviceContext);
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
                    _windowsLibrariesWrapper.User32LibraryWrapper.ReleaseDeviceContext(windowHandle, deviceContext);
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
