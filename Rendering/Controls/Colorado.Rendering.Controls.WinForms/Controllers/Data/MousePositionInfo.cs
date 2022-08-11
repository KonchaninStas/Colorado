using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.Data
{
    internal interface IMousePositionInfo
    {
        IPoint2D CursorPositionInScreenCoordinates { get; }
        IPoint2D CursorPositionInViewportCoordinates { get; }
        IRay Ray { get; }
    }

    internal class MousePositionInfo : IMousePositionInfo
    {
        internal MousePositionInfo(MouseEventArgs mouseEventArgs, IViewport viewPort)
        {
            CursorPositionInScreenCoordinates = new Point2D(mouseEventArgs.X, mouseEventArgs.Y);
            CursorPositionInViewportCoordinates = new Point2D((double)mouseEventArgs.X / viewPort.Width * viewPort.TargetPlaneWidth,
                (double)mouseEventArgs.Y / viewPort.Height * viewPort.TargetPlaneHeight);

            Ray = viewPort.CalculateCursorRay(CursorPositionInScreenCoordinates);
        }

        public IPoint2D CursorPositionInScreenCoordinates { get; }

        public IPoint2D CursorPositionInViewportCoordinates { get; }

        public IRay Ray { get; }
    }
}
