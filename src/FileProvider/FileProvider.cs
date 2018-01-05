using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CityOs.FileServer.Core;
using CityOs.FileServer.Core.Exceptions;
using Microsoft.Extensions.Options;
using MimeMapping;

namespace CityOs.FileServer.FileProvider
{
    public class FileProvider : IFileProvider
    {
        private readonly IOptions<FileProviderOptions> _options;

        private const int ChunkSize = 1024;

        public FileProvider(IOptions<FileProviderOptions> options)
        {
            _options = options;
        }

        public virtual async Task<string> Save(IFile file)
        {
            file.Id = Guid.NewGuid();
            var mimeType = MimeUtility.GetMimeMapping(file.FileExtension);

            if (string.IsNullOrWhiteSpace(mimeType))
            {
                throw new UnknownFileExtensionException($"this extension is unknown : {file.FileExtension}");
            }

            if (mimeType != file.MimeType)
            {
                throw new InconsistentMimeTypeException($"The extension and mime type are different (MimeType : ${mimeType} ,extension : ${file.FileExtension}");
            }

            var filename = $"{file.Id}.{file.FileExtension}";
            var path = GetDirectoryStorage(filename);


            using (var stream = new FileStream(path, FileMode.Append))
            {
                var buffer = new byte[ChunkSize];
                var bytesRead = 0;
                do
                {
                    bytesRead = await file.FileStream.ReadAsync(buffer, 0, buffer.Length);
                    await stream.WriteAsync(buffer, 0, bytesRead);

                } while (bytesRead > 0);
            }

            return path;
        }


        protected virtual string GetDirectoryStorage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(_options.Value.DirectoryForFileStorage))
            {
                throw new NullDirectoryFileStorageException("They are no directory specified for file storage please configure it");
            }

            if (!Directory.Exists(_options.Value.DirectoryForFileStorage))
            {
                Directory.CreateDirectory(_options.Value.DirectoryForFileStorage);
            }

            return Path.Combine(_options.Value.DirectoryForFileStorage, fileName);
        }
    }
}
