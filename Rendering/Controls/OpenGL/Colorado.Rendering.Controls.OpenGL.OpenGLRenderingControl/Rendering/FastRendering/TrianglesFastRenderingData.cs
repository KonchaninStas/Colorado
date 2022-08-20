using Colorado.Common.Extensions;
using Colorado.Geometry.Structures.GeometryProviders;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering.FastRendering
{
    internal class TrianglesFastRenderingData : FastRenderingData
    {
        #region Private fields

        private readonly IList<Triangle> _triangles;

        #endregion Private fields

        #region Constructor

        public TrianglesFastRenderingData(ITrianglesGeometryProvider trianglesGeometryProvider)
            : base(trianglesGeometryProvider.Triangles.Count * 3)
        {
            _triangles = trianglesGeometryProvider.Triangles;
            InitArrays();
        }

        #endregion Constructor

        public override Primitive Primitive => Primitive.Triangles;

        #region Protected logic

        protected override void InitArrays()
        {
            int lastAddedNormalIndex = 0;
            int lastAddedVertexIndex = 0;
            int lastAddedColorIndex = 0;
            _triangles.ForEach(t => AddTriangleValues(t, ref lastAddedNormalIndex, ref lastAddedVertexIndex, ref lastAddedColorIndex));
        }

        #endregion Protected logic

        #region Private logic

        private void AddTriangleValues(Triangle triangle, ref int lastAddedNormalIndex, ref int lastAddedVertexIndex, ref int lastAddedColorIndex)
        {
            for (int i = 0; i < 3; i++)
            {
                _normalsValuesArray[lastAddedNormalIndex++] = triangle.Normal.X;
                _normalsValuesArray[lastAddedNormalIndex++] = triangle.Normal.Y;
                _normalsValuesArray[lastAddedNormalIndex++] = triangle.Normal.Z;

                AddColorValues(triangle.Color, ref lastAddedColorIndex);
            }

            AddVertices(triangle, ref lastAddedVertexIndex);
        }

        private void AddVertices(Triangle triangle, ref int lastAddedVertexIndex)
        {
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.FirstVertex.X;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.FirstVertex.Y;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.FirstVertex.Z;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.SecondVertex.X;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.SecondVertex.Y;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.SecondVertex.Z;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.ThirdVertex.X;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.ThirdVertex.Y;
            _verticesValuesArray[lastAddedVertexIndex++] = triangle.ThirdVertex.Z;
        }

        #endregion Private logic
    }
}
