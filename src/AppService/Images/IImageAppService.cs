using CityOs.FileServer.Dto;
using System.Threading.Tasks;

namespace CityOs.FileServer.AppService
{
    public interface IImageAppService
    {
        /// <summary>
        /// Delete an image asynchronously
        /// </summary>
        /// <param name="fileName">The filename to delete</param>
        /// <returns></returns>
        Task DeleteImageAsync(string fileName);

        /// <summary>
        /// Gets stream function of a file name async
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="imageQuery">The image query</param>
        /// <returns></returns>
        Task<FileInformationDto> GetStreamByFileNameAsync(string fileName, ImageQueryDto imageQuery);
        
        /// <summary>
        /// Save a file asynchronously
        /// </summary>
        /// <param name="fileInformationDto">The file information data transfert object</param>
        /// <returns></returns>
        Task<SavedImageDto> SaveImageAsync(FileInformationDto fileInformationDto);
    }
}
