using System;

namespace Colorado.FileReaders.STLFileReaders.Exceptions
{
    internal class StlFileIsInvalidException : Exception
    {
        public StlFileIsInvalidException(Exception ex) : base(string.Empty, ex)
        {
        }
    }
}
