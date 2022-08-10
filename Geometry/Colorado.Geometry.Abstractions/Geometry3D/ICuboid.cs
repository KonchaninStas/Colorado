using Colorado.Geometry.Abstractions.Primitives;
using System.Collections.Generic;

namespace Colorado.Geometry.Abstractions.Geometry3D
{
    public interface ICuboid
    {
        IEnumerable<ILine> Lines { get; }
    }
}
