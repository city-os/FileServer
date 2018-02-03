using AutoMapper;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Dto;
using System.Threading.Tasks;

namespace CityOs.FileServer.AppService
{
    public class ImageAppService : AppServiceBase, IImageAppService
    {
        /// <summary>
        /// The image repository
        /// </summary>
        private readonly IImageRepository _imageRepository;

        /// <summary>
        /// Initialize a default <see cref="ImageAppService"/>
        /// </summary>
        /// <param name="imageRepository">The <see cref="IImageRepository"/></param>
        public ImageAppService(IImageRepository imageRepository, IMapper mapper) : base(mapper)
        {
            _imageRepository = imageRepository;
        }

        /// <inheritdoc />
        public Task DeleteImageAsync(string fileName)
        {
            return _imageRepository.DeleteImageAsync(fileName);
        }

        /// <inheritdoc />
        public async Task<FileInformationDto> GetStreamByFileNameAsync(string fileName, ImageQueryDto imageQueryDto)
        {
            var imageQuery = Mapper.Map<ImageQuery>(imageQueryDto);

            var fileInformation = await _imageRepository.GetImageByNameAsync(fileName, imageQuery);

            var fileInformationDto = Mapper.Map<FileInformationDto>(fileInformation);

            return fileInformationDto;
        }

        /// <inheritdoc />
        public async Task<SavedImageDto> SaveImageAsync(FileInformationDto fileInformationDto)
        {
            var fileInformation = Mapper.Map<FileInformation>(fileInformationDto);

            var savedImageUrl = await _imageRepository.SaveImageAsync(fileInformation);

            return new SavedImageDto { RelativeUrl = savedImageUrl };
        }
    }

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
