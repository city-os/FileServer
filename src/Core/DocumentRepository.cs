using System;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.Core
{
    public class DocumentRepository : IDocumentRepository
    {

        private readonly IFileProvider _fileProvider;

        public DocumentRepository(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<Stream> LoadDocumentAsync(IFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> GetDocumentByIdAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> InsertDocumentAsync(Stream fileStream, string mimeType, string fileName, string altAttribute = null,
            string tittleAttribut = null)
        {
            var file = FileFactory.CreateFile(fileStream, mimeType, fileName, altAttribute, tittleAttribut);
            return await _fileProvider.Save(file);
        }

        public async Task<string> UpdateDocumentAsync(Guid fileId, Stream fileStream, string mimetype, string fileName, string altAttribut = null,
            string titleAttribut = null)
        {
            throw new NotImplementedException();
        }

        public async Task ValidateDocumentAsync(Stream fileStream, string mimetype)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteDocumentAsync(IFile file)
        {
            throw new NotImplementedException();
        }
    }
}
