using AutoMapper;
using CityOs.FileServer.AppService;
using CityOs.FileServer.AppService.Adapters;
using CityOs.FileServer.Domain.Services;
using CityOs.FileServer.Infrastructure.Repositories;
using CityOs.FileServer.Provider.Core;
using CityOs.FileServer.Tests.Helpers;
using CityOs.FileServer.Tests.Mocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CityOs.FileServer.Tests.AppService
{
    [TestClass]
    public class DocumentAppServiceTests
    {
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            Mapper.Initialize(config => config.AddProfile<FileServerProfile>());
        }

        [TestMethod]
        public void Should_CreateNewFileVersion_When_FileDoNotExist()
        {
            var appService = GetDefaultImageAppService();
            var document = FileHelper.GetEmbeddedStream("Space_large.jpeg");
            var result = appService.SaveDocumentAsync(document, "Space_large.jpeg", "image/jpeg").Result;
            
            result.Should().BeEquivalentTo("Space_large.1.jpeg");
        }


        private IDocumentAppService GetDefaultImageAppService(IFileDomainService mockFileDomainService = null, IFileServerProvider mockFileServerProvider = null)
        {
            var fileServerProvider = mockFileServerProvider ?? new MockFileServerProvider();
            var documentDomainService = mockFileDomainService ?? new FileDomainService();
            var repository = new DocumentRepository(fileServerProvider);
            var appService = new DocumentAppService(repository, documentDomainService);

            return appService;
        }
    }
}