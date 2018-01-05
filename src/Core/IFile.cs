using System;
using System.IO;

namespace CityOs.FileServer.Core
{
    public interface IFile
    {
        Guid Id { get; set; }

        Stream FileStream { get; set; }

        string MimeType { get; set; }

        string FileName { get; set; }

        string AltAttribute { get; set; }

        string TitleAttribut { get; set; }

        string FileExtension { get; set; }

        string RelativePath { get; set; }
    }
}
