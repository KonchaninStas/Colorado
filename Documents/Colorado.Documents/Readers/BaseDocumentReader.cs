using Colorado.Documents.ModelStructure;
using Colorado.Documents.Structures;
using System.Collections.Generic;

namespace Colorado.Documents.Readers
{
    public interface IDocumentReader
    {
        IDocumentType DocumentType { get; }
        IEnumerable<string> DefaultDocumentsNames { get; }

        IDocument Read(string pathToFile);
    }

    public abstract class BaseDocumentReader : IDocumentReader
    {
        #region Properties

        public abstract IDocumentType DocumentType { get; }

        public abstract IEnumerable<string> DefaultDocumentsNames { get; }

        #endregion Properties

        #region Public logic

        public IDocument Read(string pathToFile)
        {
            return new Document(pathToFile, DocumentType, ReadModel(pathToFile));
        }

        protected abstract IModel ReadModel(string pathToFile);

        #endregion Public logic
    }
}
