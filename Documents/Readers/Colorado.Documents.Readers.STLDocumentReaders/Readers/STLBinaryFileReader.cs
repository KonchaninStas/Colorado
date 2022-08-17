﻿using Colorado.Documents.Readers.Exceptions;
using Colorado.Geometry.Structures.Primitives;
using Colorado.MeshStructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace Colorado.Documents.Readers.STLDocumentReader.Readers
{
    internal class STLBinaryFileReader
    {
        private readonly string pathToStlFile;
        private readonly List<Triangle> triangles;
        private int byteIndex;

        public STLBinaryFileReader(string pathToStlFile)
        {
            this.pathToStlFile = pathToStlFile;
            triangles = new List<Triangle>();
            byteIndex = 0;
        }

        public IMesh Read()
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(pathToStlFile);

                /* 80 bytes title + 4 byte num of triangles + 50 bytes (1 of triangular mesh)  */
                if (fileBytes.Length > 120)
                {
                    int numOfTriangles = GetNumberOfTriangles(fileBytes);
                    //ProgressTracker.Instance.Init(numOfTriangles);

                    byteIndex = 84;

                    for (int i = 0; i < numOfTriangles; i++)
                    {

                        /* this try-catch block will be reviewed */
                        try
                        {
                            Vector normal = GetNormal(fileBytes);
                            Point vertex1 = GetPoint(fileBytes);
                            Point vertex2 = GetPoint(fileBytes);
                            Point vertex3 = GetPoint(fileBytes);

                            byteIndex += 2; // Attribute byte count

                            triangles.Add(new Triangle(vertex1, vertex2, vertex3, normal));
                        }
                        catch
                        {
                            break;
                        }
                        finally
                        {
                            //ProgressTracker.Instance.NextStep(Resources.ReadingTriangles);
                        }
                    }
                }
                else
                {
                    // nitentionally left blank
                }
                return new Mesh(triangles);
            }
            catch (OperationAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FileIsInvalidException(ex);
            }
        }

        private Vector GetNormal(byte[] fileBytes)
        {
            GetData(fileBytes, out double x, out double y, out double z);

            return new Vector(x, y, z);
        }


        private Point GetPoint(byte[] fileBytes)
        {
            GetData(fileBytes, out double x, out double y, out double z);

            return new Point(x, y, z);
        }

        private void GetData(byte[] fileBytes, out double x, out double y, out double z)
        {
            x = GetDoubleValue(fileBytes);
            byteIndex += 4;
            y = GetDoubleValue(fileBytes);
            byteIndex += 4;
            z = GetDoubleValue(fileBytes);
            byteIndex += 4;
        }

        private float GetDoubleValue(byte[] fileBytes)
        {
            return BitConverter.ToSingle(new byte[] { fileBytes[byteIndex], fileBytes[byteIndex + 1], fileBytes[byteIndex + 2], fileBytes[byteIndex + 3] }, 0);
        }

        private int GetNumberOfTriangles(byte[] fileBytes)
        {
            byte[] temp = new byte[4];

            temp[0] = fileBytes[80];
            temp[1] = fileBytes[81];
            temp[2] = fileBytes[82];
            temp[3] = fileBytes[83];

            return BitConverter.ToInt32(temp, 0);
        }
    }
}
