using Colorado.Geometry.Structures.Math;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Ray
    {
        #region Constructors

        public Ray(Vector direction) : this(Point.Zero, direction)
        { }

        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        #endregion Constructors

        #region Properties

        public Point Origin { get; }

        public Vector Direction { get; }

        #endregion Properties

        #region Public logic

        public Ray ApplyTramsform(ITransform transform)
        {
            return new Ray(transform.Apply(Origin), transform.Apply(Direction));
        }

        #endregion Public logic
    }
}
