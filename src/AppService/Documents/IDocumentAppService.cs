using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.AppService
{
    public interface IDocumentAppService
    {
        /// <summary>
        /// Get the image stream function of the image identifier
        /// </summary>
        /// <param name="imageName">The image identifier</param>
        /// <returns></returns>
        Task<Stream> GetImageStreamByIdentifierAsync(string imageName);

        /// <summary>
        /// Save an image asynchronously
        /// </summary>
        /// <param name="stream">The stream to use</param>
        /// <returns></returns>
        Task<string> SaveImageAsync(Stream stream, string fileName, string contentType);

        /// <summary>
        /// Delete an image asynchronously
        /// </summary>
        /// <param name="imageName">The image name</param>
        /// <returns></returns>
        Task DeleteImageAsync(string imageName);
    }
}
