using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Geometry.Structures.Geometry3D
{
    public interface ICuboid
    {
        IEnumerable<Line> Lines { get; }
    }

    public class Cuboid : ICuboid
    {
        #region Protected fields

        protected readonly Point[] vertices;

        #endregion Protected fields

        #region Constructors

        public Cuboid(double width, double height, double depth, Point centerPoint, ITransform transform)
        {
            vertices = transform.Apply(GetVertices(width, height, depth, centerPoint)).ToArray();
            Lines = GetLines(vertices);
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<Line> Lines { get; }

        #endregion Properties

        #region Private logic

        private IEnumerable<Line> GetLines(Point[] vertices)
        {
            return new Line[]
            {
                new Line(vertices[0], vertices[1]), new Line(vertices[2], vertices[3]),
                new Line(vertices[0], vertices[2]), new Line(vertices[1], vertices[3]),
                new Line(vertices[4], vertices[5]), new Line(vertices[6], vertices[7]),
                new Line(vertices[4], vertices[6]), new Line(vertices[5], vertices[7]),
                new Line(vertices[2], vertices[6]), new Line(vertices[0], vertices[4]),
                new Line(vertices[1], vertices[5]), new Line(vertices[3], vertices[7]),
            };
        }

        private Point[] GetVertices(double width, double height, double depth, Point centerPoint)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            double halfDepth = depth / 2;

            return new Point[]
            {
                new Point(centerPoint.X - halfWidth, centerPoint.Y + halfHeight, centerPoint.Z - halfDepth),
                new Point(centerPoint.X - halfWidth, centerPoint.Y + halfHeight, centerPoint.Z + halfDepth),
                new Point(centerPoint.X - halfWidth, centerPoint.Y - halfHeight, centerPoint.Z - halfDepth),
                new Point(centerPoint.X - halfWidth, centerPoint.Y - halfHeight, centerPoint.Z + halfDepth),
                new Point(centerPoint.X + halfWidth, centerPoint.Y + halfHeight, centerPoint.Z - halfDepth),
                new Point(centerPoint.X + halfWidth, centerPoint.Y + halfHeight, centerPoint.Z + halfDepth),
                new Point(centerPoint.X + halfWidth, centerPoint.Y - halfHeight, centerPoint.Z - halfDepth),
                new Point(centerPoint.X + halfWidth, centerPoint.Y - halfHeight, centerPoint.Z + halfDepth),
            };
        }

        #endregion Private logic
    }
}
