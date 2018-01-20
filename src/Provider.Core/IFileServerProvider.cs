using CityOs.FileServer.Domain.Entities;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.Provider.Core
{
    public interface IFileServerProvider
    {
        /// <summary>
        /// Write a file asynchronously
        /// </summary>
        /// <param name="fileStream">The file stream</param>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        Task WriteFileAsync(Stream fileStream, string fileName);

        /// <summary>
        /// Delete a file asynchronously
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        Task DeleteFileAsync(string fileName);

        /// <summary>
        /// Gets a file function of the identifier
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        Task<Stream> GetFileByIdentifierAsync(string fileName);
    }
}
