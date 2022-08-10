using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Line : ILine
    {
        #region Constructors

        public Line(IPoint start, IPoint end)
        {
            Start = start;
            End = end;
        }

        #endregion Constructors

        #region Properties

        public IPoint Start { get; }

        public IPoint End { get; }

        #endregion Properties

        #region Public logic

        public ILine ApplyTramsform(ITransform transform)
        {
            return new Line(transform.Apply(Start), transform.Apply(End));
        }

        #endregion Public logic
    }
}
