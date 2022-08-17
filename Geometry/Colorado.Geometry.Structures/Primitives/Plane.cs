using Colorado.Geometry.Structures.Math;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Plane
    {
        private readonly Vector _normalVector;
        private readonly Point _planePoint;

        #region Construtors

        public Plane(Point origin, Vector direction)
        {
            _normalVector = new Vector(direction.X, direction.Y, direction.Z);
            _planePoint = new Point(origin.X, origin.Y, origin.Z);
        }

        #endregion Constructors

        #region Properties

        public Vector NormalVector => _normalVector;

        public Point PlanePoint => _planePoint;

        #endregion Properties

        #region Public logic

        public Plane ApplyTramsform(ITransform transform)
        {
            return new Plane(transform.Apply(PlanePoint), transform.Apply(NormalVector));
        }

        public Point GetIntersectionPoint(Ray mouseRay)
        {
            double t = (PlanePoint - mouseRay.Origin).DotProduct(NormalVector) /
                NormalVector.DotProduct(mouseRay.Direction);
            return mouseRay.Origin + (mouseRay.Direction * t);
        }

        #endregion Public logic
    }
}
