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
        public static IServiceCollection AddFileServer(this IServiceCollection services, Action<IFileServerBuilder> options)
        {
            var fileServerBuilder = new FileServerBuilder(services);

            options.Invoke(fileServerBuilder);

            fileServerBuilder.TryAddCoreServices();

            return services;
        }
    }
}
