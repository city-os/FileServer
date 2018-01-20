using System;
using System.IO;
using System.Threading.Tasks;
using CityOs.FileServer.Crosscutting.Helpers;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Provider.Core;
using ImageSharp;
using ImageSharp.Formats;
using ImageSharp.Processing;

namespace CityOs.FileServer.Infrastructure.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        /// <summary>
        /// The file server provider
        /// </summary>
        private readonly IFileServerProvider _fileServerProvider;

        /// <summary>
        /// Initialize a default <see cref="ImageRepository"/>
        /// </summary>
        /// <param name="fileServerProvider">The provider who server file</param>
        public ImageRepository(IFileServerProvider fileServerProvider)
        {
            _fileServerProvider = fileServerProvider;
        }

        /// <inheritdoc />
        public async Task<string> SaveImageAsync(FileInformation fileInformation)
        {
            var uniqueFileName = StringHelper.GetUniqueFileName();

            string fileExtension = Path.GetExtension(fileInformation.OriginalFileName);

            var newFileName = uniqueFileName + fileExtension;
            var newThumbFileName = uniqueFileName + "_thumb" + fileExtension;

            using (fileInformation.Stream)
            {
                await SaveStreamToFileAsync(fileInformation.Stream, newFileName);

                await SaveThumbnailIfNeededAsync(fileInformation.Stream, newThumbFileName);
            }

            return string.Empty;
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
                if (image.Height > 1000 || image.Width > 1000)
                {
                    var resizeOptions = new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new SixLabors.Primitives.Size(1000, 1000)
                    };

                    image.Resize(resizeOptions);

                    using (var memoryStream = new MemoryStream())
                    {
                        var extension = Path.GetExtension(newThumbFileName);

                        IImageFormat format = null;

                        switch (extension)
                        {
                            case ".jpg":
                                format = ImageFormats.Jpeg;
                                break;
                            case ".jpeg":
                                format = ImageFormats.Jpeg;
                                break;
                            case ".png":
                                format = ImageFormats.Png;
                                break;
                            default:
                                break;
                        }

                        if (format == null) throw new Exception("Format non reconnu");

                        image.Save(memoryStream, format);

                        await SaveStreamToFileAsync(memoryStream, newThumbFileName);
                    }
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

        /// <inheritdoc />
        public async Task<Stream> GetStreamByFileNameAsync(string fileName, ImageQuery imageQuery)
        {
            using (var fileStream = await _fileServerProvider.GetFileByIdentifierAsync(fileName))
            using (var image = Image.Load(fileStream))
            {
                var resizeOptions = new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new SixLabors.Primitives.Size(imageQuery.Width, imageQuery.Height)
                };

                image.Resize(resizeOptions);

                var memoryStream = new MemoryStream();

                var extension = Path.GetExtension(fileName);

                IImageFormat format = null;

                switch (extension)
                {
                    case ".jpg":
                        format = ImageFormats.Jpeg;
                        break;
                    case ".jpeg":
                        format = ImageFormats.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormats.Png;
                        break;
                    default:
                        break;
                }

                if (format == null) throw new Exception("Format non reconnu");

                image.Save(memoryStream, format);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return memoryStream;
            }
        }
    }
}
