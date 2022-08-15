using System.IO;

namespace Colorado.Documents.Structures
{
    public interface IDocumentType
    {
        string Extension { get; }
        string Name { get; }

        bool IsFileExtensionEqual(string pathToFile);
    }

    public class DocumentType : IDocumentType
    {
        public DocumentType(string typeName, string extension)
        {
            Name = typeName;
            Extension = extension;
        }

        public string Name { get; }

        public string Extension { get; }

        public bool IsFileExtensionEqual(string pathToFile)
        {
            return Path.GetExtension(pathToFile) == Extension;
        }
    }
}
