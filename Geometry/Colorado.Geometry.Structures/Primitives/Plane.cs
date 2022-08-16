using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Plane : IPlane
    {
        private readonly Vector _normalVector;
        private readonly Point _planePoint;

        #region Construtors

        public Plane(IPoint origin, IVector direction)
        {
            _normalVector = new Vector(direction.X, direction.Y, direction.Z);
            _planePoint = new Point(origin.X, origin.Y, origin.Z);
        }

        #endregion Constructors

        #region Properties

        public IVector NormalVector => _normalVector;

        public IPoint PlanePoint => _planePoint;

        #endregion Properties

        #region Public logic

        public IPlane ApplyTramsform(ITransform transform)
        {
            return new Plane(transform.Apply(PlanePoint), transform.Apply(NormalVector));
        }

        public IPoint GetIntersectionPoint(IRay mouseRay)
        {
            double t = PlanePoint.Minus(mouseRay.Origin).DotProduct(NormalVector) /
                NormalVector.DotProduct(mouseRay.Direction);
            return mouseRay.Origin.Plus(mouseRay.Direction.Multiply(t));
        }

        #endregion Public logic
    }
}
