using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CityOs.FileServer.Provider.Core;

namespace CityOs.FileServer.Tests.Mocks
{
    internal class MockFileServerProvider : IFileServerProvider
    {
        public Task DeleteFileAsync(string fileName)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        private Stream GetEmbeddedFileStream(string fileName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resource = string.Format("CityOs.FileServer.Tests.Data.{0}", fileName);

            var stream = executingAssembly.GetManifestResourceStream(resource);
            
            return stream;
        }
    }
}
