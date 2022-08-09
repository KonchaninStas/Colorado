using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Geometry.Structures.Extensions
{
    public static class PointExtensions
    {
        public static IPoint GetPointWithMinValues(this IEnumerable<IPoint> points)
        {
            return new Point(GetValuesFromPoints(points, p => p.X).Min(), GetValuesFromPoints(points, p => p.Y).Min(),
               GetValuesFromPoints(points, p => p.Z).Min());
        }

        public static IPoint GetPointWithMaxValues(this IEnumerable<IPoint> points)
        {
            return new Point(GetValuesFromPoints(points, p => p.X).Max(), GetValuesFromPoints(points, p => p.Y).Max(),
               GetValuesFromPoints(points, p => p.Z).Max());
        }

        private static IEnumerable<double> GetValuesFromPoints(IEnumerable<IPoint> points, Func<IPoint, double> getValueFromPointAction)
        {
            return points.Select(p => getValueFromPointAction(p));
        }
    }
}
