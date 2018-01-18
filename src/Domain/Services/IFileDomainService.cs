using CityOs.FileServer.Domain.Entities;

namespace CityOs.FileServer.Domain.Services
{
    public interface IFileDomainService
    {
        /// <summary>
        /// Validate the file information
        /// </summary>
        /// <param name="fileInformation"></param>
        void ValidateFileInformation(FileInformation fileInformation);
    }
}
