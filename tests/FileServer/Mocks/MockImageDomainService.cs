using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Domain.Services;

namespace CityOs.FileServer.Tests.Mocks
{
    class MockImageDomainService : IImageDomainService
    {
        private readonly ImageDomainService _imageDomainService;

        public bool ThumbnailUsed { get; private set; }

        public bool ThumbnailGenerated { get; private set; }

        public MockImageDomainService()
        {
            _imageDomainService = new ImageDomainService();
        }

        public string GetFileThumbnailName(string fileName)
        {
            return _imageDomainService.GetFileThumbnailName(fileName);
        }

        public bool ShouldResize(ImageQuery imageQuery, int maxHeight, int maxWidth)
        {
            return _imageDomainService.ShouldResize(imageQuery, maxHeight, maxWidth);
        }

        public bool UseThumbnail(ImageQuery imageQuery)
        {
            var useThumbnail = _imageDomainService.UseThumbnail(imageQuery);
            ThumbnailUsed = useThumbnail;

            return useThumbnail;
        }

        public bool UseThumbnail(int height, int width)
        {
            var useThumbnail = _imageDomainService.UseThumbnail(height, width);
            ThumbnailUsed = useThumbnail;

            return useThumbnail;
        }

        public bool GenerateThumbnail(int height, int width)
        {
            var generateThumbnail = _imageDomainService.GenerateThumbnail(height, width);
            ThumbnailGenerated = generateThumbnail;

            return generateThumbnail;
        }

        public ImageQuery GetDefaultThumbnailSize()
        {
            return _imageDomainService.GetDefaultThumbnailSize();
        }
    }
}
