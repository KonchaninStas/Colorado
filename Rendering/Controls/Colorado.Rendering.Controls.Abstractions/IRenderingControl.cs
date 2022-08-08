using Colorado.Common.Colours;
using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public interface IRenderingControl : IDisposable
    {
        RGB BackgroundColor { get; }

        void DrawScene();
        void Initialize(IntPtr windowHandle);
    }
}
