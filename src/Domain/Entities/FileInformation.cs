using System.IO;

namespace CityOs.FileServer.Domain.Entities
{
    public class FileInformation
    {
        /// <summary>
        /// Initialize a default <see cref="FileInformation"/>
        /// </summary>
        /// <param name="stream">The file stream</param>
        /// <param name="originalFileName">The original file name</param>
        /// <param name="fileType">The file type</param>
        public FileInformation(Stream stream, string originalFileName, string fileType)
        {
            Stream = stream;
            OriginalFileName = originalFileName;
            FileType = fileType;
        }

        /// <summary>
        /// Gets the file stream
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Gets the original file name
        /// </summary>
        public string OriginalFileName { get; }

        /// <summary>
        /// Gets the file type
        /// </summary>
        public string FileType { get; }
    }
}
