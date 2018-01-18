using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Provider.Core;

namespace CityOs.FileServer.Provider.FileSystem
{
    internal class FileSystemProvider : IFileServerProvider
    {
        /// <summary>
        /// The base folder to get images
        /// </summary>
        private readonly string _baseFolder;

        /// <summary>
        /// Initialize a default <see cref="FileSystemProvider"/>
        /// </summary>
        /// <param name="baseFolder">The base folder</param>
        public FileSystemProvider(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        /// <inheritdoc />
        public Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_baseFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Stream> GetFileByIdentifierAsync(string fileName)
        {
            var filePath = Path.Combine(_baseFolder, fileName);

            if (File.Exists(filePath))
            {
                Stream file = File.OpenRead(filePath);

                return Task.FromResult(file);
            }

            return Task.FromResult<Stream>(null);
        }

        /// <inheritdoc />
        public async Task<string> WriteFileAsync(FileInformation fileInformation)
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
