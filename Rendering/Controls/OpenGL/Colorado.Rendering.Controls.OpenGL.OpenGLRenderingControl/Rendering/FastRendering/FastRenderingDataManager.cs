using Colorado.Common.Extensions;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering
{
    internal interface IFastRenderingDataManager
    {
        IFastRenderingData this[IGeometryProvider geometryProvider] { get; }
    }

    internal class FastRenderingDataManager : IFastRenderingDataManager
    {
        private readonly IDictionary<IGeometryProvider, FastRenderingData> _geometryProviderToFastRenderingDataMap;

        private FastRenderingDataManager()
        {
            _geometryProviderToFastRenderingDataMap = new Dictionary<IGeometryProvider, FastRenderingData>();
        }

        private static IFastRenderingDataManager _instance;

        public static IFastRenderingDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FastRenderingDataManager();
                }

                return _instance;
            }
        }

        public IFastRenderingData this[IGeometryProvider geometryProvider]
        {
            get
            {
                return _geometryProviderToFastRenderingDataMap.GetOrAdd(geometryProvider, (m) =>
                {
                    if (geometryProvider is ILinesGeometryProvider linesGeometryProvider)
                    {
                        return new LinesFastRenderingData(linesGeometryProvider);
                    }
                    else if (geometryProvider is ITrianglesGeometryProvider trianglesGeometryProvider)
                    {
                        return new TrianglesFastRenderingData(trianglesGeometryProvider);
                    }
                    else
                    {
                        throw new System.Exception("Unsupported geometry provider.");
                    }

                });
            }
        }
    }
}
