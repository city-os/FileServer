using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Provider.Core;

namespace CityOs.FileServer.Infrastructure.Repositories
{
    internal class DocumentRepository : IDocumentRepository
    {
        /// <summary>
        /// The file server provider
        /// </summary>
        private readonly IFileServerProvider _fileServerProvider;

        /// <summary>
        /// Initialize a default <see cref="DocumentRepository"/>
        /// </summary>
        /// <param name="baseFolder">The base folder to work on</param>
        public DocumentRepository(IFileServerProvider fileServerProvider)
        {
            _fileServerProvider = fileServerProvider;
        }

        /// <inheritdoc />
        public Task DeleteImageAsync(string imageName)
        {
            return _fileServerProvider.DeleteFileAsync(imageName);
        }

        /// <inheritdoc />
        public Task<Stream> GetFileStreamByIdentifierAsync(string fileIdentifier)
        {
            return _fileServerProvider.GetFileByIdentifierAsync(fileIdentifier);
        }

        /// <inheritdoc />
        public Task<string> SaveImageAsync(FileInformation fileInformation)
        {
            return _fileServerProvider.WriteFileAsync(fileInformation);
        }
    }
}
