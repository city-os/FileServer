using System.IO;
using System.Linq;

namespace CityOs.FileServer.Core
{
    public static class FileFactory
    {
        public static IFile CreateFile(Stream fileStream, string mimeType, string fileName, string altAttribute = null,
            string tittleAttribut = null)
        {

            var fileExtension = fileName.Split('.').LastOrDefault();

            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                //todo throw ex
            }

            return new File()
            {
                AltAttribute = altAttribute,
                FileExtension = fileExtension,
                FileStream = fileStream,
                MimeType = mimeType,
                FileName = fileName,
                TitleAttribut = tittleAttribut
            };
        }
    }
}
