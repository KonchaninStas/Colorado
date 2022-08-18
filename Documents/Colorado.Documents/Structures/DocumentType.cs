using System;
using System.IO;

namespace Colorado.Documents.Structures
{
    public interface IDocumentType : IEquatable<IDocumentType>
    {
        string Extension { get; }
        string Name { get; }

        bool IsFileExtensionEqual(string pathToFile);
    }

    public class DocumentType : IDocumentType
    {
        #region Constructor

        public DocumentType(string typeName, string extension)
        {
            Name = typeName;
            Extension = extension;
        }

        #endregion Constructor

        #region Properties

        public string Name { get; }

        public string Extension { get; }

        #endregion Properties

        #region Public logic

        public bool IsFileExtensionEqual(string pathToFile)
        {
            return Path.GetExtension(pathToFile) == Extension;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((IDocumentType)obj);
        }

        public bool Equals(IDocumentType other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name && Extension == other.Extension;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Extension.GetHashCode();
        }

        #endregion Public logic
    }
}
