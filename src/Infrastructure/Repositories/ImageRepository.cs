using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using CityOs.FileServer.Crosscutting.Helpers;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Domain.Services;
using CityOs.FileServer.Provider.Core;
using ImageSharp;
using ImageSharp.Formats;
using ImageSharp.Processing;
using MimeMapping;

namespace CityOs.FileServer.Infrastructure.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        /// <summary>
        /// The file server provider
        /// </summary>
        private readonly IFileServerProvider _fileServerProvider;

        /// <summary>
        /// The image domain service
        /// </summary>
        private readonly IImageDomainService _imageDomainService;

        /// <summary>
        /// Initialize a default <see cref="ImageRepository"/>
        /// </summary>
        /// <param name="fileServerProvider">The provider who server file</param>
        public ImageRepository(IFileServerProvider fileServerProvider, IImageDomainService imageDomainService)
        {
            _fileServerProvider = fileServerProvider;
            _imageDomainService = imageDomainService;
        }

        /// <inheritdoc />
        public async Task<string> SaveImageAsync(FileInformation fileInformation)
        {
            var uniqueFileName = StringHelper.GetUniqueFileName();

            string fileExtension = Path.GetExtension(fileInformation.OriginalFileName);

            var newFileName = uniqueFileName + fileExtension;
            var newThumbFileName = _imageDomainService.GetFileThumbnailName(newFileName);

            using (fileInformation.Stream)
            {
                await SaveStreamToFileAsync(fileInformation.Stream, newFileName);

                await SaveThumbnailIfNeededAsync(fileInformation.Stream, newThumbFileName);
            }

            return string.Empty;
        }

        /// <inheritdoc />
        public async Task DeleteImageAsync(string fileName)
        {
            var thumbnailFileName = _imageDomainService.GetFileThumbnailName(fileName);
            var thumbnailExists = await _fileServerProvider.FileExists(thumbnailFileName);

            using(var transactionScope = new TransactionScope())
            {
                await _fileServerProvider.DeleteFileAsync(fileName);

                if (thumbnailExists)
                {
                    await _fileServerProvider.DeleteFileAsync(thumbnailFileName);
                }

                transactionScope.Complete();
            }
        }

        /// <inheritdoc />
        public async Task<FileInformation> GetImageByNameAsync(string fileName, ImageQuery imageQuery)
        {
            var fileNameToUse = await GetFileNameToUse(fileName, imageQuery);

            if (fileNameToUse == null) return null;

            using (var fileStream = await _fileServerProvider.GetFileByIdentifierAsync(fileNameToUse))
            using (var image = Image.Load(fileStream))
            {
                IImageFormat format = Image.DetectFormat(fileStream);

                var stream = GetResizeStream(image, format, imageQuery);

                var extension = Path.GetExtension(fileName);

                return new FileInformation(stream, fileName, MimeUtility.GetMimeMapping(extension));
            }
        }

        /// <summary>
        /// Save a thumbnail if needed
        /// </summary>
        /// <param name="stream">The stream to use</param>
        /// <param name="newThumbFileName">The thumbnail file name</param>
        /// <returns></returns>
        private async Task SaveThumbnailIfNeededAsync(Stream stream, string newThumbFileName)
        {
            stream.Seek(0, SeekOrigin.Begin);

            using (var image = Image.Load(stream))
            {
                var shouldSaveThumbnail = _imageDomainService.GenerateThumbnail(image.Height, image.Width);

                if (!shouldSaveThumbnail) return;

                IImageFormat format = Image.DetectFormat(stream);

                var thumbnailDefaultSize = _imageDomainService.GetDefaultThumbnailSize();

                using (var resizeStream = GetResizeStream(image, format, thumbnailDefaultSize))
                {
                    await SaveStreamToFileAsync(resizeStream, newThumbFileName);
                }
            }
        }

        /// <summary>
        /// Write a stream to a file
        /// </summary>
        /// <param name="stream">The stream to write</param>
        /// <param name="fileName">The file name</param>
        private async Task SaveStreamToFileAsync(Stream stream, string fileName)
        {
            stream.Seek(0, SeekOrigin.Begin);

            await _fileServerProvider.WriteFileAsync(stream, fileName);
        }
        
        /// <summary>
        /// Gets the resized image stream
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="format">The image format</param>
        /// <param name="imageQuery">The image query</param>
        /// <returns></returns>
        private Stream GetResizeStream(Image<Rgba32> image, IImageFormat format, ImageQuery imageQuery)
        {
            var memoryStream = new MemoryStream();

            var shouldResize = _imageDomainService.ShouldResize(imageQuery, image.Height, image.Width);

            if (shouldResize)
            {
                var resizeOptions = new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new SixLabors.Primitives.Size(imageQuery.Width, imageQuery.Height)
                };

                image.Resize(resizeOptions);
            }

            image.Save(memoryStream, format);

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        /// <summary>
        /// Get the file name to use
        /// </summary>
        /// <param name="fileName">The file name to check</param>
        /// <param name="imageQuery">The image query</param>
        /// <returns></returns>
        private async Task<string> GetFileNameToUse(string fileName, ImageQuery imageQuery)
        {
            var useThumbnail = _imageDomainService.UseThumbnail(imageQuery);

            if (useThumbnail)
            {
                var thumbnailName = _imageDomainService.GetFileThumbnailName(fileName);

                var thumbnailExists = await _fileServerProvider.FileExists(thumbnailName);

                if (thumbnailExists)
                {
                    return thumbnailName;
                }
            }

            var originalFileExists = await _fileServerProvider.FileExists(fileName);

            if (originalFileExists)
            {
                return fileName;
            }

            return null;
        }
    }
}
