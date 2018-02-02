using System.IO;

namespace CityOs.FileServer.Dto
{
    public class FileInformationDto
    {
        /// <summary>
        /// Gets the file stream
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// Gets the original file name
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets the file type
        /// </summary>
        public string FileType { get; set; }
    }
}
