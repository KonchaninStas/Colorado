using Colorado.Geometry.Structures.Math;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Line
    {
        #region Constructors

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        #endregion Constructors

        #region Properties

        public Point Start { get; }

        public Point End { get; }

        #endregion Properties

        #region Public logic

        public Line ApplyTramsform(ITransform transform)
        {
            return new Line(transform.Apply(Start), transform.Apply(End));
        }

        #endregion Public logic
    }
}
