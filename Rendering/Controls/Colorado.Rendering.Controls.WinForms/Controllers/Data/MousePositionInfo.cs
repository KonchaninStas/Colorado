using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.Abstractions.Scene;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.Data
{
    internal interface IMousePositionInfo
    {
        Point2D CursorPositionInScreenCoordinates { get; }
        Point2D CursorPositionInViewportCoordinates { get; }
        Ray Ray { get; }
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

        public Point2D CursorPositionInScreenCoordinates { get; }

        public Point2D CursorPositionInViewportCoordinates { get; }

        public Ray Ray { get; }
    }
}
