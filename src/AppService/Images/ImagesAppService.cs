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
        public async Task<FileInformationDto> GetFileInfoByNameAsync(string fileName, ImageQueryDto imageQueryDto)
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
}
