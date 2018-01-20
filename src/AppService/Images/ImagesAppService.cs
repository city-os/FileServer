using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Dto;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.AppService
{
    public class ImageAppService : IImageAppService
    {
        /// <summary>
        /// The image repository
        /// </summary>
        private readonly IImageRepository _imageRepository;

        /// <summary>
        /// Initialize a default <see cref="ImageAppService"/>
        /// </summary>
        /// <param name="imageRepository">The <see cref="IImageRepository"/></param>
        public ImageAppService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        /// <inheritdoc />
        public Task<Stream> GetStreamByFileNameAsync(string fileName, ImageQueryDto imageQuery)
        {
            var imageQueryEntity = new ImageQuery
            {
                Height = imageQuery.Height,
                Width = imageQuery.Width
            };

            return _imageRepository.GetStreamByFileNameAsync(fileName, imageQueryEntity);
        }

        /// <inheritdoc />
        public async Task<SavedImageDto> SaveImageAsync(Stream imageStream, string fileName, string contentType)
        {
            var fileInformation = new FileInformation(imageStream, fileName, contentType);

            var savedImageUrl = await _imageRepository.SaveImageAsync(fileInformation);

            return new SavedImageDto { RelativeUrl = savedImageUrl };
        }
    }

    public interface IImageAppService
    {
        /// <summary>
        /// Gets stream function of a file name async
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="imageQuery">The image query</param>
        /// <returns></returns>
        Task<Stream> GetStreamByFileNameAsync(string fileName, ImageQueryDto imageQuery);

        /// <summary>
        /// Save an image asynchronously
        /// </summary>
        /// <param name="imageStream">The image stream</param>
        /// <param name="fileName">The image file name</param>
        /// <param name="contentType">The image content type</param>
        /// <returns>The saved image informations</returns>
        Task<SavedImageDto> SaveImageAsync(Stream imageStream, string fileName, string contentType);
    }
}
