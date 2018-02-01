using AutoMapper;
using CityOs.FileServer.AppService;
using CityOs.FileServer.AppService.Adapters;
using CityOs.FileServer.Dto;
using CityOs.FileServer.Infrastructure.Repositories;
using CityOs.FileServer.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CityOs.FileServer.Tests.AppService
{
    [TestClass]
    public class ImageAppServiceTests
    {
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
        public async Task Should_GetImage_When_QueryHaveRightInformations()
        {
            const string fileName = "Rioji.png";

            var appService = GetDefaultImageAppService();

            var fileInformation = await appService.GetStreamByFileNameAsync(fileName, GetDefaultImageQueryDto());

            Assert.IsNotNull(fileInformation);
            Assert.AreEqual(fileInformation.OriginalFileName, fileName);
            Assert.AreEqual(fileInformation.FileType, "image/png");
            Assert.IsNotNull(fileInformation.Stream);
        }

        [TestMethod]
        public async Task Should_ReturnNull_When_FileNotExists()
        {
            const string fileName = "Tata.jpeg";

            var appService = GetDefaultImageAppService();

            var fileInformation = await appService.GetStreamByFileNameAsync(fileName, GetDefaultImageQueryDto());

            Assert.IsNull(fileInformation);
        }

        /// <summary>
        /// Get a default <see cref="ImageAppService"/>
        /// </summary>
        /// <returns></returns>
        private ImageAppService GetDefaultImageAppService()
        {
            var fileServerProvider = new MockFileServerProvider();
            var repository = new ImageRepository(fileServerProvider);
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
                Width = 100,
                Height = 100
            };
        }
    }
}
