using Colorado.Geometry.Structures.Extensions;
using Colorado.Geometry.Structures.Geometry3D;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Geometry.Structures.BoundingBoxStructures
{
    public interface IBoundingBox
    {
        Point Center { get; }
        double Diagonal { get; }
        bool IsEmpty { get; }
        Point MaxPoint { get; }
        Point MinPoint { get; }
        ICuboid Cuboid { get; }
        double SphereRadius { get; }

        IBoundingBox Add(IBoundingBox boundingBox);
        IBoundingBox ApplyTransform(ITransform transform);
        IBoundingBox Clone();
        void ResetToDefault();
    }

    public class BoundingBox : IBoundingBox
    {
        #region Constructors

        public BoundingBox() : this(Point.Zero, Point.Zero) { }

        public BoundingBox(Point maxPoint, Point minPoint)
        {
            Init(maxPoint, minPoint);
        }

        public BoundingBox(ICollection<Triangle> triangles)
        {
            IEnumerable<Point> points = null;
            if (triangles.Count == 0)
            {
                points = new Point[] { new Point(-1, -1, -1), new Point(1, 1, 1) };
            }
            else
            {
                points = triangles.SelectMany(t => new[] { t.FirstVertex, t.SecondVertex, t.ThirdVertex });
            }

            Init(points.GetPointWithMaxValues(), points.GetPointWithMinValues());
        }

        #endregion Constructors

        #region Properties

        public Point MaxPoint { get; private set; }

        public Point MinPoint { get; private set; }

        public bool IsEmpty => MaxPoint.Equals(MinPoint);

        public Point Center { get; private set; }

        public double Diagonal { get; private set; }

        public double SphereRadius { get; private set; }

        public ICuboid Cuboid { get; private set; }

        #endregion Properties

        #region Public logic

        public IBoundingBox Add(IBoundingBox boundingBox)
        {
            if (IsEmpty)
            {
                return new BoundingBox(boundingBox.MaxPoint, boundingBox.MinPoint);
            }
            else
            {
                return new BoundingBox(new[] { MaxPoint, boundingBox.MaxPoint }.GetPointWithMaxValues(),
                    new[] { MinPoint, boundingBox.MinPoint }.GetPointWithMinValues());
            }
        }

        public IBoundingBox ApplyTransform(ITransform transform)
        {
            return new BoundingBox(transform.Apply(MaxPoint), transform.Apply(MinPoint));
        }

        public void ResetToDefault()
        {
            Init(Point.Zero, Point.Zero);
        }

        public IBoundingBox Clone()
        {
            return new BoundingBox(MaxPoint, MinPoint);
        }

        #endregion Public logic

        #region Private logic

        private void Init(Point maxPoint, Point minPoint)
        {
            var points = new[] { maxPoint, minPoint };
            MaxPoint = points.GetPointWithMaxValues();
            MinPoint = points.GetPointWithMinValues();

            var diagonalVector = new Vector(minPoint, maxPoint);

            Diagonal = diagonalVector.Length;
            Center = GetCenterPoint();
            SphereRadius = MaxPoint.DistanceTo(Center);
            Cuboid = new Cuboid(GetWidth(), GetHeight(), GetDepth(), Center, Transform.Identity());

        }

        private Point GetCenterPoint()
        {
            return (MinPoint + MaxPoint) / 2;
        }

        private double GetDepth()
        {
            return MaxPoint.Z - MinPoint.Z;
        }

        private double GetHeight()
        {
            return MaxPoint.Y - MinPoint.Y;
        }

        private double GetWidth()
        {
            return MaxPoint.X - MinPoint.X;
        }

        #endregion Private logic
    }
}
