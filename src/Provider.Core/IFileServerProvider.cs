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

        /// <summary>
        /// Check if the file exist
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        Task<bool> FileExists(string fileName);

        /// <summary>
        /// Gets the new file version if file already exist asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task<int> GetNewFileVersionIfFileAlreadyExistAsync(string fileName);

        /// <summary>
        /// Gets the last file version asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task<Stream> GetLastFileVersionAsync(string fileName);
    }
}
