using Colorado.Common.Colours;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public abstract class RenderingControl : IRenderingControl
    {
        protected RenderingControl()
        {
            BackgroundColor = RGB.RedColor;
        }

        public RGB BackgroundColor { get; }

        public abstract void DrawScene();
        public abstract void Initialize(IntPtr windowHandle);
        public abstract void Dispose();
    }
}
