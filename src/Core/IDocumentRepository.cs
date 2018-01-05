using System;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.Core
{
    public interface IDocumentRepository
    {
        Task<Stream> LoadDocumentAsync(IFile file);

        Task<IFile> GetDocumentByIdAsync(Guid fileId);

        Task<string> InsertDocumentAsync(Stream fileStream, string mimeType, string fileName, string altAttribute = null,string tittleAttribut = null);

        Task<string> UpdateDocumentAsync(Guid fileId, Stream fileStream, string mimetype, string fileName,string altAttribut = null, string titleAttribut = null);

        Task ValidateDocumentAsync(Stream fileStream, string mimetype);

        Task DeleteDocumentAsync(IFile file);
    }
}
