using Colorado.Common.Colours;
using Colorado.Rendering.Controls.Abstractions.Scene;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public interface IRenderingControl : IDisposable
    {
        RGB BackgroundColor { get; }
        IViewport Viewport { get; }

        void Initialize(IntPtr windowHandle);

        void BeforeDrawScene();
        void DrawSceneGeometry();
        void DrawScenePrimitives();
        void EndDrawScene();
        void DisableLighting();
    }
}
