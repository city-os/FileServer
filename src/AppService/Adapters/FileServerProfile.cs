using AutoMapper;
using CityOs.FileServer.Domain.Entities;
using CityOs.FileServer.Dto;

namespace CityOs.FileServer.AppService.Adapters
{
    public class FileServerProfile : Profile
    {
        public FileServerProfile()
        {
            CreateMap<FileInformationDto, FileInformation>();
            CreateMap<ImageQueryDto, ImageQuery>();
        }
    }
}
