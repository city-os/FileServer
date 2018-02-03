using AutoMapper;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Domain.Services;
using CityOs.FileServer.Dto;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.AppService
{
    internal class DocumentAppService : IDocumentAppService
    {
        /// <summary>
        /// The repository who manage documents
        /// </summary>
        private readonly IDocumentRepository _documentRepository;

        /// <summary>
        /// The file domain service
        /// </summary>
        private readonly IFileDomainService _fileDomainService;

        /// <summary>
        /// Initialize a default <see cref="DocumentAppService"/>
        /// </summary>
        /// <param name="documentRepository">The repository who manage documents</param>
        public DocumentAppService(IDocumentRepository documentRepository, IFileDomainService fileDomainService)
        {
            _documentRepository = documentRepository;
            _fileDomainService = fileDomainService;
        }

        /// <inheritdoc />
        public Task DeleteImageAsync(string imageName)
        {
            return _documentRepository.DeleteDocumentAsync(imageName);
        }

        /// <inheritdoc />
        public async Task<FileInformationDto> GetFileInfoByNameAsync(string imageName)
        {
            var fileInformation = await _documentRepository.GetDocumentByNameAsync(imageName);

            var fileInformationDto = Mapper.Map<FileInformationDto>(fileInformation);

            return fileInformationDto;
        }

        /// <inheritdoc />
        public Task<string> SaveImageAsync(Stream stream, string fileName, string contentType)
        {
            var fileInfo = new FileInformation(stream, fileName, contentType);

            _fileDomainService.ValidateFileInformation(fileInfo);

            return _documentRepository.SaveDocumentAsync(fileInfo);
        }
    }
}
