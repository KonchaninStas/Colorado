using Colorado.Application;
using Colorado.Common.Colours;
using Colorado.Rendering.Controls.Abstractions.Scene;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public interface IRenderingControl : IDisposable
    {
        IRGB BackgroundColor { get; }
        IProgram Program { get; }
        IViewport Viewport { get; }
        void Initialize(IntPtr windowHandle);
        void DrawScene();
    }
}
