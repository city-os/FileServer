using CityOs.FileServer.Domain.Entities;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.Domain.Contracts
{
    public interface IImageRepository
    {
        /// <summary>
        /// Save an image asynchronously
        /// </summary>
        /// <param name="fileInformation">The file information</param>
        /// <returns></returns>
        Task<string> SaveImageAsync(FileInformation fileInformation);

        /// <summary>
        /// Gets image stream function of a filename
        /// </summary>
        /// <param name="fileName">The file name to retrieve</param>
        /// <returns></returns>
        Task<FileInformation> GetStreamByFileNameAsync(string fileName, ImageQuery imageQuery);
    }
}
