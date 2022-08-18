using Colorado.Application;
using Colorado.Common.Colours;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Controls.Abstractions.Statistics;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public interface IRenderingControl : IDisposable
    {
        IRGB BackgroundColor { get; }
        IProgram Program { get; }
        IViewport Viewport { get; }

        IRenderingControlStatistics RenderingControlStatistics { get; }

        event EventHandler DrawSceneStarted;
        event EventHandler DrawSceneFinished;

        void Initialize(IntPtr windowHandle);
        void DrawScene();
    }
}
