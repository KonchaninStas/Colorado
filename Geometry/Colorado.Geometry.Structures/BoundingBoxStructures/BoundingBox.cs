using Colorado.Geometry.Abstractions.BoundingBoxStructures;
using Colorado.Geometry.Abstractions.Geometry3D;
using Colorado.Geometry.Abstractions.Math;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Extensions;
using Colorado.Geometry.Structures.Geometry3D;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Geometry.Structures.BoundingBoxStructures
{
    public class BoundingBox : IBoundingBox
    {
        #region Constructors

        public BoundingBox() : this(Point.ZeroPoint, Point.ZeroPoint) { }

        public BoundingBox(IPoint maxPoint, IPoint minPoint)
        {
            Init(maxPoint, minPoint);
        }

        public BoundingBox(ICollection<ITriangle> triangles)
        {
            IEnumerable<IPoint> points = null;
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

        public IPoint MaxPoint { get; private set; }

        public IPoint MinPoint { get; private set; }

        public bool IsEmpty => MaxPoint.Equals(MinPoint);

        public IPoint Center { get; private set; }

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
            Init(Point.ZeroPoint, Point.ZeroPoint);
        }

        public IBoundingBox Clone()
        {
            return new BoundingBox(MaxPoint, MinPoint);
        }

        #endregion Public logic

        #region Private logic

        private void Init(IPoint maxPoint, IPoint minPoint)
        {
            var points = new[] { maxPoint, minPoint };
            MaxPoint = points.GetPointWithMaxValues();
            MinPoint = points.GetPointWithMinValues();

            var diagonalVector = new Vector(minPoint, maxPoint);

            Diagonal = diagonalVector.Length;
            Center = MinPoint.Plus(MaxPoint).Divide(2);
            SphereRadius = MaxPoint.DistanceTo(Center);
            Cuboid = new Cuboid(GetWidth(), GetHeight(), GetDepth(),
                MinPoint.Plus(MaxPoint).Divide(2), Transform.Identity());

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
