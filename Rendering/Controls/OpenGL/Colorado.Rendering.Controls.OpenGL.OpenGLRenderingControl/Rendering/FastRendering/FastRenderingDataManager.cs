using Colorado.Common.Extensions;
using Colorado.MeshStructure;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering
{
    internal interface IFastRenderingDataManager
    {
        IFastRenderingData this[IMesh mesh] { get; }
    }

    internal class FastRenderingDataManager : IFastRenderingDataManager
    {
        private readonly IDictionary<IMesh, FastRenderingData> _meshToFastRenderingDataMap = new Dictionary<IMesh, FastRenderingData>();

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

        public IFastRenderingData this[IMesh mesh]
        {
            get
            {
                return _meshToFastRenderingDataMap.GetOrAdd(mesh, (m) => new FastRenderingData(m.Triangles));
            }
        }
    }
}
