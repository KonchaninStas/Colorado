using Colorado.Common.Colours;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering
{
    internal abstract class FastRenderingData : IFastRenderingData
    {
        #region Private fields

        protected readonly double[] _verticesValuesArray;
        protected readonly double[] _normalsValuesArray;
        protected readonly byte[] _verticesColorsValuesArray;

        #endregion Private fields

        #region Constructor

        public FastRenderingData(int verticesCount)
        {
            _verticesValuesArray = new double[verticesCount * 3];
            _normalsValuesArray = new double[verticesCount * 3];
            _verticesColorsValuesArray = new byte[verticesCount * 4];
        }

        #endregion Constructor

        #region Properties

        public double[] VerticesValuesArray => _verticesValuesArray;

        public double[] NormalsValuesArray => _normalsValuesArray;

        public byte[] VerticesColorsValuesArray => _verticesColorsValuesArray;

        public int VerticesCount => VerticesValuesArray.Length / 3;

        public abstract Primitive Primitive { get; }

        #endregion Properties

        #region Protected logic

        protected abstract void InitArrays();

        protected void AddColorValues(IRGB color, ref int lastAddedColorIndex)
        {
            _verticesColorsValuesArray[lastAddedColorIndex++] = color.Red;
            _verticesColorsValuesArray[lastAddedColorIndex++] = color.Green;
            _verticesColorsValuesArray[lastAddedColorIndex++] = color.Blue;
            _verticesColorsValuesArray[lastAddedColorIndex++] = (byte)(color.Intensity * byte.MaxValue);
        }

        #endregion Protected logic
    }
}
