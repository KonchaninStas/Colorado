using Colorado.Documents.ModelStructure;
using Colorado.Documents.Structures;

namespace Colorado.Documents
{
    public interface IDocument
    {
        IDocumentType DocumentType { get; }
        IModel Model { get; }
        string Path { get; }
    }

    public class Document : IDocument
    {
        #region Constructor

        public Document(string pathToFile, IDocumentType documentType, IModel model)
        {
            Path = pathToFile;
            DocumentType = documentType;
            Model = model;
        }

        #endregion Constructor

        #region Properties

        public string Path { get; }

        public IDocumentType DocumentType { get; }

        public IModel Model { get; }

        #endregion Properties
    }
}
