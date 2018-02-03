using System.IO;
using System.Threading.Tasks;
using ChinhDo.Transactions;
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
        /// The transactional file manager
        /// </summary>
        private readonly TxFileManager _transactionFileManager;

        /// <summary>
        /// Initialize a default <see cref="FileSystemProvider"/>
        /// </summary>
        /// <param name="baseFolder">The base folder</param>
        public FileSystemProvider(string baseFolder)
        {
            _baseFolder = baseFolder;

            _transactionFileManager = new TxFileManager();
        }

        /// <inheritdoc />
        public Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_baseFolder, fileName);

            if (File.Exists(filePath))
            {
                _transactionFileManager.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<bool> FileExists(string fileName)
        {
            var filePath = Path.Combine(_baseFolder, fileName);

            var exists = File.Exists(filePath);

            return Task.FromResult(exists);
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
        public async Task WriteFileAsync(Stream fileStream, string fileName)
        {
            var filePath = Path.Combine(_baseFolder, fileName);

            using (var file = File.Create(filePath))
            {
                fileStream.Seek(0, SeekOrigin.Begin);

                await fileStream.CopyToAsync(file);
            }
        }
    }
}
