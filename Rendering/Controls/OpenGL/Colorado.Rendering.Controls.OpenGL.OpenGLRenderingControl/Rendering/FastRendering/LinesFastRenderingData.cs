using Colorado.Common.Colours;
using Colorado.Common.Extensions;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering
{
    internal class LinesFastRenderingData : FastRenderingData
    {
        #region Private fields

        private readonly IList<Line> _lines;
        private readonly IRGB _color;

        #endregion Private fields

        #region Constructor

        public LinesFastRenderingData(ILinesGeometryProvider linesGeometryProvider)
            : base(linesGeometryProvider.Lines.Count * 6)
        {
            _lines = linesGeometryProvider.Lines;
            _color = linesGeometryProvider.Material.Diffuse;
            InitArrays();
        }

        public override Primitive Primitive => Primitive.Lines;

        #endregion Constructor

        #region Protected logic

        protected override void InitArrays()
        {
            int lastAddedVertexIndex = 0;

            AddColorValues(_color);
            _lines.ForEach(l => AddLineValues(l, ref lastAddedVertexIndex));
        }

        #endregion Protected logic

        #region Private logic

        private void AddLineValues(Line line, ref int lastAddedVertexIndex)
        {
            AddVertices(line, ref lastAddedVertexIndex);
        }

        private void AddColorValues(IRGB color)
        {
            int lastAddedColorIndex = 0;
            for (int i = 0; i < _verticesColorsValuesArray.Length / 3; i++)
            {
                _verticesColorsValuesArray[lastAddedColorIndex++] = color.Red;
                _verticesColorsValuesArray[lastAddedColorIndex++] = color.Green;
                _verticesColorsValuesArray[lastAddedColorIndex++] = color.Blue;
            }
        }

        private void AddVertices(Line line, ref int lastAddedVertexIndex)
        {
            _verticesValuesArray[lastAddedVertexIndex++] = line.Start.X;
            _verticesValuesArray[lastAddedVertexIndex++] = line.Start.Y;
            _verticesValuesArray[lastAddedVertexIndex++] = line.Start.Z;
            _verticesValuesArray[lastAddedVertexIndex++] = line.End.X;
            _verticesValuesArray[lastAddedVertexIndex++] = line.End.Y;
            _verticesValuesArray[lastAddedVertexIndex++] = line.End.Z;
        }

        #endregion Private logic
    }
}
