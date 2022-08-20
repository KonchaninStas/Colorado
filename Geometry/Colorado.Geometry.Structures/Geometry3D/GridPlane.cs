using Colorado.Common.Colours;
using Colorado.Common.Extensions;
using Colorado.Geometry.Materials;
using Colorado.Geometry.Structures.BaseStructures;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.Extensions;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Geometry.Structures.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Geometry.Structures.Geometry3D
{
    public interface IGridPlane : IRenderableObject
    {
        IRGB Color { get; }
        IList<Line> Lines { get; }
    }

    public sealed class GridPlane : RenderableObject, IGridPlane
    {
        #region Private fields

        #endregion Private fields

        #region Constructor

        public GridPlane() : this(5, 100, 0) { }

        public GridPlane(int space, double size, double zValue)
        {
            Visible = true;
            Color = RGB.GridDefaultColor;
            //Color.Changed += (s, e) => PrepareLines(space, size, zValue);
            Lines = PrepareLines(space, size, zValue);

            IEnumerable<Point> points = Lines.TakeLast(4).Select(l => new Point[] { l.Start, l.End }).SelectMany(l => l);
            BoundingBox = new BoundingBox(points.GetPointWithMaxValues(), points.GetPointWithMinValues());

            GeometryProvider = new LinesGeometryProvider(Lines, new Material(Color));
        }

        #endregion Constructor

        #region Properties

        public IRGB Color { get; }

        public override IBoundingBox BoundingBox { get; }

        public IList<Line> Lines { get; }

        public override IGeometryProvider GeometryProvider { get; }

        #endregion Properties

        #region Private logic

        private IList<Line> PrepareLines(int space, double size, double zValue)
        {
            int updatedSize = ((int)(size / space)) * space;
            var linesList = new List<Line>();

            int numberOfLines = updatedSize / space;
            if (numberOfLines < 1)
            {
                numberOfLines = 1;
            }
            for (int x = 0; x < numberOfLines; x++)
            {
                linesList.Add(new Line(new Point(-updatedSize, x * space, zValue), new Point(updatedSize, x * space, zValue)));
                linesList.Add(new Line(new Point(-updatedSize, x * -space, zValue), new Point(updatedSize, x * -space, zValue)));

                linesList.Add(new Line(new Point(x * space, -updatedSize, zValue), new Point(x * space, updatedSize, zValue)));
                linesList.Add(new Line(new Point(x * -space, -updatedSize, zValue), new Point(x * -space, updatedSize, zValue)));
            };

            linesList.Add(new Line(new Point(-updatedSize, numberOfLines * space, zValue), new Point(updatedSize, numberOfLines * space, zValue)));
            linesList.Add(new Line(new Point(-updatedSize, numberOfLines * -space, zValue), new Point(updatedSize, numberOfLines * -space, zValue)));

            linesList.Add(new Line(new Point(numberOfLines * space, -updatedSize, zValue), new Point(numberOfLines * space, updatedSize, zValue)));
            linesList.Add(new Line(new Point(numberOfLines * -space, -updatedSize, zValue), new Point(numberOfLines * -space, updatedSize, zValue)));

            return linesList;
        }

        #endregion Private logic
    }
}
