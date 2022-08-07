using System;

namespace Colorado.Rendering.Controls.Abstractions
{
    public interface IRenderingControl : IDisposable
    {
        void DrawScene();
        void Initialize(IntPtr windowHandle);
    }
}
