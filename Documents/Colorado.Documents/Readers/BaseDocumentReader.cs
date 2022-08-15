using Colorado.Documents.Structures;
using Colorado.ModelStructure;
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
        protected BaseDocumentReader()
        {

        }

        public IDocument Read(string pathToFile)
        {
            return new Document(pathToFile, DocumentType, ReadModel(pathToFile));
        }

        public abstract IDocumentType DocumentType { get; }

        protected abstract IModel ReadModel(string pathToFile);

        public abstract IEnumerable<string> DefaultDocumentsNames { get; }
    }
}
