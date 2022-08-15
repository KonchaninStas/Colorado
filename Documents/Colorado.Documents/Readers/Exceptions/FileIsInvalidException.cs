using System;

namespace Colorado.Documents.Readers.Exceptions
{
    public class FileIsInvalidException : Exception
    {
        public FileIsInvalidException(Exception exception) : base(exception.Message, exception)
        {

        }
    }
}
