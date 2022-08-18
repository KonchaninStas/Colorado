using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Materials;
using System.Collections.Generic;

namespace Colorado.Geometry.Structures.GeometryProviders
{
    public interface ILinesGeometryProvider : IGeometryProvider
    {
        IList<Line> Lines { get; }
    }

    public sealed class LinesGeometryProvider : GeometryProvider, ILinesGeometryProvider
    {
        #region Constructor

        public LinesGeometryProvider(IList<Line> lines, IMaterial material) : base(material)
        {
            Lines = lines;
        }

        #endregion Constructor

        #region Properties

        public IList<Line> Lines { get; }

        #endregion Properties
    }
}
