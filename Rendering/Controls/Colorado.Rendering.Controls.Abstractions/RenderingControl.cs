using Colorado.Common.Colours;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Lighting;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public abstract class RenderingControl : IRenderingControl
    {
        protected readonly ILightsManager _lightsManager;

        protected RenderingControl(ILightsManager lightsManager, IViewport viewport)
        {
            _lightsManager = lightsManager;
            Viewport = viewport;
            BackgroundColor = RGB.RedColor;
        }

        public IViewport Viewport { get; }

        public RGB BackgroundColor { get; }

        public abstract void Initialize(IntPtr windowHandle);

        public abstract void Dispose();

        public abstract void BeforeDrawScene();

        public abstract void DrawSceneGeometry();

        public abstract void DrawScenePrimitives();

        public abstract void EndDrawScene();

        public void DisableLighting()
        {
            _lightsManager.DisableLighting();
        }
    }
}
