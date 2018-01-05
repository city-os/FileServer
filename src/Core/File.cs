using System;
using System.IO;

namespace CityOs.FileServer.Core
{
    public class File : IFile
    {
        public Guid Id { get; set; }
        public Stream FileStream { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribut { get; set; }
        public string FileExtension { get; set; }
        public string RelativePath { get; set; }
    }
}
