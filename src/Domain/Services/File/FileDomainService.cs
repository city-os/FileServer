using CityOs.FileServer.Crosscutting.Exceptions;
using CityOs.FileServer.Domain.Entities;
using MimeMapping;
using System.IO;

namespace CityOs.FileServer.Domain.Services
{
    internal class FileDomainService : IFileDomainService
    {
        /// <inheritdoc />
        public void ValidateFileInformation(FileInformation fileInformation)
        {
            var fileExtension = Path.GetExtension(fileInformation.OriginalFileName);

            var mimeType = MimeUtility.GetMimeMapping(fileExtension);

            if (string.IsNullOrEmpty(mimeType))
            {
                throw new FileServerException("Unknown mime type");
            }

            if(!(mimeType == fileInformation.FileType))
            {
               throw new FileServerException("The mime type differ from the content type send");
            }
        }
    }
}
