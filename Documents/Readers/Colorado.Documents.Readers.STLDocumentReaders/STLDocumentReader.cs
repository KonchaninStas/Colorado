using Colorado.Documents.Readers.STLDocumentReader.Readers;
using Colorado.Documents.Structures;
using Colorado.MeshStructure;
using Colorado.ModelStructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Colorado.Documents.Readers.STLDocumentReader
{
    public class STLDocumentReader : BaseDocumentReader
    {
        private readonly STLDocumentType _stlDocumentType;

        public STLDocumentReader()
        {
            _stlDocumentType = new STLDocumentType();

            DefaultDocumentsNames = ReadContentFiles();
        }

        public override IDocumentType DocumentType => _stlDocumentType;

        public override IEnumerable<string> DefaultDocumentsNames { get; }

        protected override IModel ReadModel(string pathToFile)
        {
            STLFileType fileType = STLFileUtil.GetStlFileType(pathToFile);

            IMesh mesh = fileType == STLFileType.ASCII ?
                new STLASCIIFileReader(pathToFile).Read() : new STLBinaryFileReader(pathToFile).Read();

            return new Model(new Node(mesh));
        }

        private IEnumerable<string> ReadContentFiles()
        {
            string pathToContentFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources");

            return Directory.GetFiles(pathToContentFolder).Where(f => _stlDocumentType.IsFileExtensionEqual(f));
        }
    }
}
