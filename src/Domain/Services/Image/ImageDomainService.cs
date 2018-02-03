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
}
