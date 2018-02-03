using CityOs.FileServer.Domain.Entities;
using System.IO;

namespace CityOs.FileServer.Domain.Services
{
    public class ImageDomainService : IImageDomainService
    {
        /// <summary>
        /// The default thumbnail size
        /// </summary>
        private readonly ImageQuery ThumbnailDefaultSize = new ImageQuery { Height = 1080, Width = 1920 };

        /// <inheritdoc />
        public ImageQuery GetDefaultThumbnailSize()
        {
            return ThumbnailDefaultSize;
        }

        /// <inheritdoc />
        public string GetFileThumbnailName(string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);

            return string.Format($"{fileNameWithoutExtension}_thumb{extension}");
        }

        /// <inheritdoc />
        public bool ShouldResize(ImageQuery imageQuery, int maxHeight, int maxWidth)
        {
            return !(imageQuery.Height == 0 && imageQuery.Width == 0) && (imageQuery.Height <= maxHeight && imageQuery.Width <= maxWidth);
        }

        /// <inheritdoc />
        public bool UseThumbnail(ImageQuery imageQuery)
        {
            if (!IsFullHd(imageQuery.Width, imageQuery.Height))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public bool GenerateThumbnail(int height, int width)
        {
            if (IsFullHd(width, height))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public bool UseThumbnail(int height, int width)
        {
            return UseThumbnail(new ImageQuery
            {
                Height = height,
                Width = width
            });
        }

        /// <summary>
        /// Check if the image is full hd
        /// </summary>
        /// <param name="firstSide">The first side</param>
        /// <param name="secondSide">The second side</param>
        /// <returns></returns>
        private bool IsFullHd(int firstSide, int secondSide)
        {
            if (firstSide > ThumbnailDefaultSize.Width || secondSide > ThumbnailDefaultSize.Height)
            {
                return true;
            }

            if (ThumbnailDefaultSize.Height > 1080 || secondSide > ThumbnailDefaultSize.Width)
            {
                return true;
            }

            return false;
        }
    }

    public interface IImageDomainService
    {
        /// <summary>
        /// Check if we can try to use thumbnail or not
        /// </summary>
        /// <param name="imageQuery">The image query</param>
        /// <returns></returns>
        bool UseThumbnail(ImageQuery imageQuery);

        /// <summary>
        /// Check if we can try to use thumbnail or not
        /// </summary>
        /// <param name="height">The image height</param>
        /// <param name="width">The image width</param>
        /// <returns></returns>
        bool UseThumbnail(int height, int width);

        /// <summary>
        /// Define if we need to generate a thumbnail for this image
        /// </summary>
        bool GenerateThumbnail(int height, int width);

        /// <summary>
        /// Gets the file thumbnail name
        /// </summary>
        /// <param name="fileName">The file name (with extension)</param>
        /// <returns></returns>
        string GetFileThumbnailName(string fileName);

        /// <summary>
        /// Check if we need to resize or not
        /// </summary>
        /// <param name="imageQuery">The image query</param>
        /// <returns></returns>
        bool ShouldResize(ImageQuery imageQuery, int maxHeight, int maxWidth);

        /// <summary>
        /// Gets the default thumbnail size
        /// </summary>
        /// <returns></returns>
        ImageQuery GetDefaultThumbnailSize();
    }
}
