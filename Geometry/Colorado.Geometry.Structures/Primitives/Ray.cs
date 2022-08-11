using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Ray : IRay
    {
        #region Constructors

        public Ray(IPoint origin, IVector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Ray(IVector direction) : this(Point.Zero, direction)
        { }

        #endregion Constructors

        #region Properties

        public IPoint Origin { get; }

        public IVector Direction { get; }

        #endregion Properties

        #region Public logic

        public IRay ApplyTramsform(ITransform transform)
        {
            return new Ray(transform.Apply(Origin), transform.Apply(Direction));
        }

        #endregion Public logic
    }
}
