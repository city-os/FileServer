using AutoMapper;
using CityOs.FileServer.AppService;
using CityOs.FileServer.AppService.Adapters;
using CityOs.FileServer.Domain.Services;
using CityOs.FileServer.Dto;
using CityOs.FileServer.Infrastructure.Repositories;
using CityOs.FileServer.Tests.Helpers;
using CityOs.FileServer.Tests.Mocks;
using ImageSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.Tests.AppService
{
    [TestClass]
    public class ImageAppServiceTests
    {
        public const string FileName = "Rioji.png";
        public const string JpgFileName = "Rioji.jpg";

        /// <summary>
        /// Initializes the class.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            Mapper.Initialize(config => config.AddProfile<FileServerProfile>());
        }

        [TestMethod]
        public async Task Should_GetOriginalImage_When_QueryHaveDefaultInformations()
        {
            var appService = GetDefaultImageAppService();
            var imageQuery = new ImageQueryDto();

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.IsTrue(IsImageValid(fileInformation.Stream, 480, 640));
            Assert.AreEqual(fileInformation.OriginalFileName, FileName);
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_ReturnPngType_When_QueryPngImage()
        {
            var appService = GetDefaultImageAppService();
            var imageQuery = new ImageQueryDto();

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.AreEqual(fileInformation.FileType, "image/png");
        }

        [TestMethod]
        public async Task Should_ReturnJpgType_When_QueryJpgImage()
        {
            var appService = GetDefaultImageAppService();
            var imageQuery = new ImageQueryDto();

            var fileInformation = await appService.GetStreamByFileNameAsync(JpgFileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.AreEqual(fileInformation.FileType, "image/jpeg");
        }

        [TestMethod]
        public async Task Should_GetResizeImage_When_QueryOnlyHeight()
        {
            var appService = GetDefaultImageAppService();

            var imageQuery = GetDefaultImageQueryDto();
            imageQuery.Width = 0;
            imageQuery.Height = 20;

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.IsTrue(IsImageValid(fileInformation.Stream, imageQuery.Height, null));
            Assert.AreEqual(fileInformation.OriginalFileName, FileName);
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_GetResizeImage_When_QueryOnlyWidth()
        {
            var appService = GetDefaultImageAppService();

            var imageQuery = GetDefaultImageQueryDto();
            imageQuery.Width = 300;
            imageQuery.Height = 0;

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);
                        
            Assert.IsNotNull(fileInformation);
            Assert.IsTrue(IsImageValid(fileInformation.Stream, null, imageQuery.Width));
            Assert.AreEqual(fileInformation.OriginalFileName, FileName);
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_GetOriginalImage_When_QueryTooLargeWidth()
        {
            var appService = GetDefaultImageAppService();

            var imageQuery = GetDefaultImageQueryDto();
            imageQuery.Width = 6000;
            imageQuery.Height = 0;

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.IsTrue(IsImageValid(fileInformation.Stream, null, 640));
            Assert.AreEqual(fileInformation.OriginalFileName, FileName);
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_GetOriginalImage_When_QueryTooLargeHeight()
        {
            var appService = GetDefaultImageAppService();

            var imageQuery = GetDefaultImageQueryDto();
            imageQuery.Height = 80000;
            imageQuery.Width = 0;

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.IsTrue(IsImageValid(fileInformation.Stream, 480, null));
            Assert.AreEqual(fileInformation.OriginalFileName, FileName);
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_UseThumbnail_When_AskForSmallImage()
        {
            var mockDomainService = new MockImageDomainService();

            var appService = GetDefaultImageAppService(mockDomainService);

            var imageQuery = GetDefaultImageQueryDto();
            imageQuery.Height = 20;
            imageQuery.Width = 0;

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);

            Assert.IsNotNull(fileInformation);
            Assert.IsTrue(mockDomainService.ThumbnailUsed);
            Assert.IsTrue(IsImageValid(fileInformation.Stream, 20, null));
            Assert.AreEqual(fileInformation.OriginalFileName, FileName);
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_NotUseThumbnail_When_AskForLargeImage()
        {
            var mockDomainService = new MockImageDomainService();

            var appService = GetDefaultImageAppService(mockDomainService);

            var imageQuery = GetDefaultImageQueryDto();
            imageQuery.Height = 20000;
            imageQuery.Width = 0;

            var fileInformation = await appService.GetStreamByFileNameAsync(FileName, imageQuery);
            
            Assert.IsFalse(mockDomainService.ThumbnailUsed);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_FileNotExists()
        {
            const string fileName = "Tata.jpeg";

            var appService = GetDefaultImageAppService();

            var fileInformation = await appService.GetStreamByFileNameAsync(fileName, GetDefaultImageQueryDto());

            Assert.IsNull(fileInformation);
        }

        [TestMethod]
        public async Task Should_GenerateThumbnail_When_ImageTooLarge()
        {
            var mockDomainService = new MockImageDomainService();
            var appService = GetDefaultImageAppService(mockDomainService);

            var fileStream = FileHelper.GetEmbeddedStream("Space_large.jpeg");

            var savedImage = await appService.SaveImageAsync(new FileInformationDto
            {
                FileType = "image/jpeg",
                OriginalFileName = "Space_large.jpeg",
                Stream = fileStream
            });

            Assert.IsTrue(mockDomainService.ThumbnailGenerated);
        }

        [TestMethod]
        public async Task Should_NotGenerateThumbnail_When_ImageTooSmall()
        {
            var mockDomainService = new MockImageDomainService();
            var appService = GetDefaultImageAppService(mockDomainService);

            var fileStream = FileHelper.GetEmbeddedStream(FileName);

            var savedImage = await appService.SaveImageAsync(new FileInformationDto
            {
                FileType = "image/jpeg",
                OriginalFileName = "Rioji.jpg",
                Stream = fileStream
            });

            Assert.IsFalse(mockDomainService.ThumbnailGenerated);
        }

        /// <summary>
        /// Get a default <see cref="ImageAppService"/>
        /// </summary>
        /// <returns></returns>
        private ImageAppService GetDefaultImageAppService(IImageDomainService mockImageDomainService = null)
        {
            var fileServerProvider = new MockFileServerProvider();
            var imageDomainService = mockImageDomainService ?? new ImageDomainService();
            var repository = new ImageRepository(fileServerProvider, imageDomainService);
            var appService = new ImageAppService(repository, Mapper.Instance);

            return appService;
        }

        /// <summary>
        /// Gets a default image query data transfert object
        /// </summary>
        /// <returns></returns>
        private ImageQueryDto GetDefaultImageQueryDto()
        {
            return new ImageQueryDto
            {
                Width = 640,
                Height = 480
            };
        }

        /// <summary>
        /// Check if the image is valid
        /// </summary>
        /// <param name="stream">The stream to validate</param>
        /// <param name="height">The height</param>
        /// <param name="width">The width</param>
        /// <returns></returns>
        private bool IsImageValid(Stream stream, int? height, int? width)
        {
            using (var image = Image.Load(stream))
            {
                bool heightValid = true;
                bool widthValid = true;

                if (height.HasValue)
                {
                    heightValid = image.Height == height.Value;
                }

                if (width.HasValue)
                {
                    widthValid = image.Width == width.Value;
                }

                return heightValid && widthValid;
            }
        }
    }
}
