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
        /// <param name="fileInformation">The file information</param>
        /// <returns></returns>
        Task<string> WriteFileAsync(FileInformation fileInformation);

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
