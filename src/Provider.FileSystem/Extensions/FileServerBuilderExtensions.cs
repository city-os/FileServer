using CityOs.FileServer.Distributed.Mvc;
using CityOs.FileServer.Provider.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CityOs.FileServer.Provider.FileSystem.Extensions
{
    public static class FileServerBuilderExtensions
    {
        public static IFileServerBuilder UseFileSystem(this IFileServerBuilder fileServerBuilder)
        {
            fileServerBuilder.TryAddSingleton<IFileServerProvider, FileSystemProvider>(serviceProvider =>
            {
                var hostingEnvironment = serviceProvider.GetService<IHostingEnvironment>();

                return new FileSystemProvider(hostingEnvironment.WebRootPath);
            });

            return fileServerBuilder;
        }
    }
}
