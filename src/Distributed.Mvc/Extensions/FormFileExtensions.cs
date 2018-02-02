using CityOs.FileServer.Dto;
using Microsoft.AspNetCore.Http;

namespace CityOs.FileServer.Distributed.Mvc.Extensions
{
    public static class FormFileExtensions
    {
        /// <summary>
        /// Convert a <see cref="IFormFile"/> to a <see cref="FileInformationDto"/>
        /// </summary>
        /// <param name="formFile">The form file to convert</param>
        /// <returns>The converted <see cref="FileInformationDto"/></returns>
        public static FileInformationDto ToFileInfoDto(this IFormFile formFile)
        {
            if (formFile == null) return new FileInformationDto();

            return new FileInformationDto
            {
                FileType = formFile.ContentType,
                OriginalFileName = formFile.FileName,
                Stream = formFile.OpenReadStream()
            };
        }
    }
}
