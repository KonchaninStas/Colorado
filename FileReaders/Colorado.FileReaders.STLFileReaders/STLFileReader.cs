using Colorado.FileReaders.STLFileReaders.Exceptions;
using Colorado.FileReaders.STLFileReaders.Readers;
using Colorado.MeshStructure;
using System.IO;

namespace Colorado.FileReaders.STLFileReaders
{
    public static class STLFileReader
    {
        public static IMesh Read(string pathToStlFile)
        {
            if (!File.Exists(pathToStlFile))
            {
                throw new FileNotFoundException();
            }
            else if (!STLFileUtil.IsStlFile(pathToStlFile))
            {
                throw new FileNotSupportedException();
            }
            else
            {
                STLFileType fileType = STLFileUtil.GetStlFileType(pathToStlFile);

                return fileType == STLFileType.ASCII ?
                    new STLASCIIFileReader(pathToStlFile).Read() : new STLBinaryFileReader(pathToStlFile).Read();
            }
        }
    }
}
