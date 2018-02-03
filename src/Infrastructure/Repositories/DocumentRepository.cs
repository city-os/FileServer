﻿using System.IO;
using System.Threading.Tasks;
using CityOs.FileServer.Crosscutting.Helpers;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Provider.Core;
using MimeMapping;

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
        public Task DeleteDocumentAsync(string imageName)
        {
            return _fileServerProvider.DeleteFileAsync(imageName);
        }

        /// <inheritdoc />
        public async Task<FileInformation> GetDocumentByNameAsync(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            var stream = await _fileServerProvider.GetFileByIdentifierAsync(fileName);

            return new FileInformation(stream, fileName, MimeUtility.GetMimeMapping(extension));
        }

        /// <inheritdoc />
        public async Task<string> SaveDocumentAsync(FileInformation fileInformation)
        {
            var uniqueFileName = StringHelper.GetUniqueFileName();
            var extension = Path.GetExtension(fileInformation.OriginalFileName);

            var newFileName = uniqueFileName + extension;

            await _fileServerProvider.WriteFileAsync(fileInformation.Stream, newFileName);

            return newFileName;
        }
    }
}
