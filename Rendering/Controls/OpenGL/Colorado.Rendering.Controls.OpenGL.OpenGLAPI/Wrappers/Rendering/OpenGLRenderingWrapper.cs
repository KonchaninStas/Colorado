using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Rendering.Controls.Abstractions.Rendering.Settings;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.Rendering;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Rendering
{
    public static class OpenGLRenderingWrapper
    {
        public static void DrawPoint(IPoint point, IRGB color, double size)
        {
            SetPointSize(size);
            Draw(Primitive.Points, () =>
            {
                SetVertexColour(color);
                Set3DVertex(point);
            });
            SetDefaultPointSize();
        }

        public static void DrawLine(ILine line, int width, IRGB colour)
        {
            SetLineWidth(width);
            Draw(Primitive.Lines, () =>
            {
                SetVertexColour(colour);
                Set3DVertex(line.Start);
                Set3DVertex(line.End);
            });
            SetDefaultLineWidth();
        }

        public static void Set3DVertex(IPoint point)
        {
            OpenGLRenderingAPI.SetVertex(point.X, point.Y, point.Z);
        }

        public static void SetVertexColour(IRGB colour)
        {
            OpenGLRenderingAPI.SetColor(colour.Red, colour.Green, colour.Blue);
        }

        private static void Draw(Primitive primitive, Action drawAction)
        {
            StartDrawing(primitive);
            drawAction.Invoke();
            EndDrawing();
        }

        private static void StartDrawing(Primitive primitive)
        {
            OpenGLRenderingAPI.Begin((int)primitive);
        }

        private static void EndDrawing()
        {
            OpenGLRenderingAPI.End();
        }

        public static unsafe void DrawFastRenderingData(IFastRenderingData fastRenderingData)
        {
            double[] verticesValues = fastRenderingData.VerticesValuesArray;
            double[] normalsValues = fastRenderingData.NormalsValuesArray;
            double[] colorsValues = fastRenderingData.VerticesColorsValuesArray;

            fixed (double* cachedPoints = verticesValues)
            fixed (double* cachedNormals = normalsValues)
            fixed (double* cachedColors = colorsValues)
            {
                DrawBuffers(fastRenderingData.VerticesCount, cachedPoints, cachedNormals, cachedColors);
            }
        }

        private static unsafe void DrawBuffers(int numberOfVertices, double* vertices, double* normals, double* colors)
        {
            EnableClientState(ArrayType.Vertex);
            EnableClientState(ArrayType.Normal);
            EnableClientState(ArrayType.Color);

            VertexPointer((IntPtr)vertices);
            NormalPointer((IntPtr)normals);
            ColorPointerRGB((IntPtr)colors);

            DrawArrays(Primitive.Triangles, numberOfVertices);

            DisableClientState(ArrayType.Vertex);
            DisableClientState(ArrayType.Normal);
            DisableClientState(ArrayType.Color);
        }

        private static void DrawArrays(Primitive primitive, int numberOfVertices)
        {
            OpenGLRenderingAPI.glDrawArrays((int)primitive, 0, numberOfVertices);
        }

        public static void SetPolygonMode(PolygonMode mode)
        {
            OpenGLRenderingAPI.PolygonMode((int)FaceSide.FrontAndBack, (int)mode);
        }

        public static void NormalPointer(IntPtr normals)
        {
            OpenGLRenderingAPI.NormalPointer((int)DataType.Double, 0, normals);
        }

        public static void VertexPointer(IntPtr vertices)
        {
            OpenGLRenderingAPI.VertexPointer(3, (int)DataType.Double, 0, vertices);
        }

        public static void ColorPointerRGB(IntPtr colors)
        {
            OpenGLRenderingAPI.ColorPointer(3, (int)DataType.Float, 0, colors);
        }

        public static void EnableClientState(ArrayType type)
        {
            OpenGLRenderingAPI.EnableClientState((int)type);
        }

        public static void DisableClientState(ArrayType type)
        {
            OpenGLRenderingAPI.DisableClientState((int)type);
        }

        public static void SetPointSize(double size)
        {
            OpenGLRenderingAPI.PointSize((float)size);
        }

        public static void SetLineWidth(double width)
        {
            OpenGLRenderingAPI.SetLineWidth((float)width);
        }

        public static void SetDefaultLineWidth()
        {
            OpenGLRenderingAPI.SetLineWidth(1f);
        }

        public static void SetDefaultPointSize()
        {
            OpenGLRenderingAPI.PointSize(1f);
        }
    }
}
