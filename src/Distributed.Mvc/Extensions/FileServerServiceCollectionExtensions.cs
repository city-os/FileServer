using Microsoft.Extensions.DependencyInjection;
using System;

namespace CityOs.FileServer.Distributed.Mvc.Extensions
{
    public static class FileServerServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the file server.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IFileServerBuilder AddFileServer(this IServiceCollection services)
        {
            var fileServerBuilder = new FileServerBuilder(services);

            fileServerBuilder.TryAddCoreServices();

            return fileServerBuilder;
        }
    }
}
