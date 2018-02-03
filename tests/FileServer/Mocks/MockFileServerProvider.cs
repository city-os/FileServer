using System.IO;
using System.Threading.Tasks;
using CityOs.FileServer.Provider.Core;
using CityOs.FileServer.Tests.Helpers;

namespace CityOs.FileServer.Tests.Mocks
{
    internal class MockFileServerProvider : IFileServerProvider
    {
        public Task DeleteFileAsync(string fileName)
        {
            return Task.CompletedTask;
        }

        public Task<bool> FileExists(string fileName)
        {
            var stream = GetEmbeddedFileStream(fileName);

            if(stream != null)
            {
                stream.Dispose();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<Stream> GetFileByIdentifierAsync(string fileName)
        {
            return Task.FromResult(GetEmbeddedFileStream(fileName));
        }

        public Task WriteFileAsync(Stream fileStream, string fileName)
        {
            return Task.CompletedTask;
        }

        private Stream GetEmbeddedFileStream(string fileName)
        {
            return FileHelper.GetEmbeddedStream(fileName);
        }
    }
}
