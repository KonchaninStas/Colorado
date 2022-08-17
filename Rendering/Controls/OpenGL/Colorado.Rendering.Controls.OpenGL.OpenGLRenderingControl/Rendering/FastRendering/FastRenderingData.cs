using Colorado.Common.Extensions;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering
{
    public class FastRenderingData : IFastRenderingData
    {
        #region Private fields

        private readonly double[] verticesValuesArray;
        private readonly double[] normalsValuesArray;
        private readonly double[] verticesColorsValuesArray;

        #endregion Private fields

        #region Constructor

        public FastRenderingData(IList<Triangle> triangles)
        {
            verticesValuesArray = new double[triangles.Count * 9];
            normalsValuesArray = new double[triangles.Count * 9];
            verticesColorsValuesArray = new double[triangles.Count * 9];

            int lastAddedNormalIndex = 0;
            int lastAddedVertexIndex = 0;
            int lastAddedColorIndex = 0;
            triangles.ForEach(t => AddTriangleValues(t, ref lastAddedNormalIndex, ref lastAddedVertexIndex, ref lastAddedColorIndex));
        }

        #endregion Constructor

        #region Properties

        public double[] VerticesValuesArray => verticesValuesArray;

        public double[] NormalsValuesArray => normalsValuesArray;

        public double[] VerticesColorsValuesArray => verticesColorsValuesArray;

        public int VerticesCount => VerticesValuesArray.Length / 3;

        #endregion Properties

        #region Private logic

        private void AddTriangleValues(Triangle triangle, ref int lastAddedNormalIndex, ref int lastAddedVertexIndex, ref int lastAddedColorIndex)
        {
            for (int i = 0; i < 3; i++)
            {
                normalsValuesArray[lastAddedNormalIndex++] = triangle.Normal.X;
                normalsValuesArray[lastAddedNormalIndex++] = triangle.Normal.Y;
                normalsValuesArray[lastAddedNormalIndex++] = triangle.Normal.Z;

                verticesColorsValuesArray[lastAddedColorIndex++] = triangle.Color.Red;
                verticesColorsValuesArray[lastAddedColorIndex++] = triangle.Color.Green;
                verticesColorsValuesArray[lastAddedColorIndex++] = triangle.Color.Blue;
            }

            AddVertices(triangle, ref lastAddedVertexIndex);
        }

        private void AddVertices(Triangle triangle, ref int lastAddedVertexIndex)
        {
            verticesValuesArray[lastAddedVertexIndex++] = triangle.FirstVertex.X;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.FirstVertex.Y;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.FirstVertex.Z;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.SecondVertex.X;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.SecondVertex.Y;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.SecondVertex.Z;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.ThirdVertex.X;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.ThirdVertex.Y;
            verticesValuesArray[lastAddedVertexIndex++] = triangle.ThirdVertex.Z;
        }

        #endregion Private logic
    }
}
