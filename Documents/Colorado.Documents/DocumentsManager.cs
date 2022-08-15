using Colorado.Common.Extensions;
using Colorado.Documents.Readers;
using Colorado.Documents.Readers.Exceptions;
using Colorado.Documents.Structures;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Colorado.Documents
{
    public interface IDocumentsManager
    {
        IDocument ActiveDocument { get; }

        IList<string> GetDefaultDocumentsNames();
        IDocument OpenDocument(string pathToFile);
        void RegisterFileReader(IDocumentReader documentReader);
    }

    public class DocumentsManager : IDocumentsManager
    {
        private readonly IDictionary<IDocumentType, IDocumentReader> _documentTypeToReaderMap;
        private readonly IDictionary<string, IDocument> _documentPathToDocumentMap;

        public DocumentsManager()
        {
            _documentTypeToReaderMap = new Dictionary<IDocumentType, IDocumentReader>();
            _documentPathToDocumentMap = new Dictionary<string, IDocument>();
        }

        public void RegisterFileReader(IDocumentReader documentReader)
        {
            _documentTypeToReaderMap.GetOrAdd(documentReader.DocumentType, (d) => documentReader);
        }

        public IList<string> GetDefaultDocumentsNames()
        {
            return _documentTypeToReaderMap.Values.SelectMany(r => r.DefaultDocumentsNames).ToList();
        }

        public IDocument OpenDocument(string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                throw new FileNotFoundException();
            }
            IDocumentType documentType = GetDocumentType(pathToFile);
            if (documentType == null)
            {
                throw new FileNotSupportedException();
            }

            ActiveDocument = _documentPathToDocumentMap.GetOrAdd(pathToFile,
                (p) => _documentTypeToReaderMap[documentType].Read(pathToFile));

            return ActiveDocument;
        }

        public IDocument ActiveDocument { get; private set; }

        private IDocumentType GetDocumentType(string pathToFile)
        {
            return _documentTypeToReaderMap.Keys.FirstOrDefault(d => d.IsFileExtensionEqual(pathToFile));
        }
    }
}
