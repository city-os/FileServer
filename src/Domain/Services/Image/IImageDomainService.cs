using CityOs.FileServer.Domain.Entities;

namespace CityOs.FileServer.Domain.Services
{
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
