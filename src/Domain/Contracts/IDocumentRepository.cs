using CityOs.FileServer.Domain.Entities;
using System.Threading.Tasks;

namespace CityOs.FileServer.Domain.Contracts
{
    public interface IDocumentRepository
    {
        /// <summary>
        /// Gets the file stream function of its identifier
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        Task<FileInformation> GetDocumentByNameAsync(string fileName);

        /// <summary>
        /// Save the image asynchronously
        /// </summary>
        /// <param name="fileInformation">The file information to use</param>
        /// <returns></returns>
        Task<string> SaveDocumentAsync(FileInformation fileInformation);

        /// <summary>
        /// Delete an image asynchronously
        /// </summary>
        /// <param name="imageName">The image to delete</param>
        /// <returns></returns>
        Task DeleteDocumentAsync(string imageName);

        /// <summary>
        /// Gets the last file version asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task<FileInformation> GetLastFileVersionAsync(string fileName);


        /// <summary>
        /// Gets the document byversion asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        Task<FileInformation> GetDocumentByVersionAsync(string fileName, int version);
    }
}
