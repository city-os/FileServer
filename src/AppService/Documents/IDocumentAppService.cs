using CityOs.FileServer.Dto;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.AppService
{
    public interface IDocumentAppService
    {
        /// <summary>
        /// Get the image stream function of the image identifier
        /// </summary>
        /// <param name="fileName">The image identifier</param>
        /// <returns></returns>
        Task<FileInformationDto> GetFileInfoByNameAsync(string fileName);

        /// <summary>
        /// Save an image asynchronously
        /// </summary>
        /// <param name="stream">The stream to use</param>
        /// <returns></returns>
        Task<string> SaveDocumentAsync(Stream stream, string fileName, string contentType);

        /// <summary>
        /// Delete an image asynchronously
        /// </summary>
        /// <param name="imageName">The image name</param>
        /// <returns></returns>
        Task DeleteImageAsync(string imageName);

        /// <summary>
        /// Gets the file information version asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        Task<FileInformationDto> GetFileInfoByVersionAsync(string fileName, int version);

        /// <summary>
        /// Gets the last file information version asynchronous.
        /// </summary>
        /// <param name="imageName">Name of the image.</param>
        /// <returns></returns>
        Task<FileInformationDto> GetLastFileInfoVersionAsync(string imageName);
    }
}
