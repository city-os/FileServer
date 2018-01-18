using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;

namespace CityOs.FileServer.Infrastructure.Repositories
{
    internal class DocumentRepository : IDocumentRepository
    {
        /// <summary>
        /// The base folder to get images
        /// </summary>
        private readonly string _baseFolder;

        /// <summary>
        /// Initialize a default <see cref="DocumentRepository"/>
        /// </summary>
        /// <param name="baseFolder">The base folder to work on</param>
        public DocumentRepository(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        /// <inheritdoc />
        public Task<Stream> GetFileStreamByIdentifierAsync(string fileIdentifier)
        {
            var filePath = Path.Combine(_baseFolder, fileIdentifier);

            if (File.Exists(filePath))
            {
                Stream file = File.OpenRead(filePath);

                return Task.FromResult(file);
            }

            return Task.FromResult<Stream>(null);
        }

        /// <inheritdoc />
        public async Task<string> SaveImageAsync(FileInformation fileInformation)
        {
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

            string fileExtension = Path.GetExtension(fileInformation.OriginalFileName);
            string fileName = uid + fileExtension;

            var filePath = Path.Combine(_baseFolder, fileName);

            using (var file = File.Create(filePath))
            {
                await fileInformation.Stream.CopyToAsync(file);
            }

            return fileName;
        }
    }
}
