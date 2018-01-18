using System;
using CityOs.FileServer.AppService;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Services;
using CityOs.FileServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CityOs.FileServer.Distributed.Mvc
{
    internal class FileServerBuilder : IFileServerBuilder
    {
        /// <summary>
        /// The available services
        /// </summary>
        private IServiceCollection _services;

        /// <summary>
        /// Initialize a default <see cref="FileServerBuilder"/>
        /// </summary>
        /// <param name="services">The services to populate or used</param>
        public FileServerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// Try to add core services to services collection
        /// </summary>
        internal void TryAddCoreServices()
        {
            _services.AddSingleton<IDocumentAppService, DocumentAppService>();
            _services.AddSingleton<IFileDomainService, FileDomainService>();
            _services.AddSingleton<IDocumentRepository>(provider =>
            {
                var env = provider.GetService<IHostingEnvironment>();
                return new DocumentRepository(env.WebRootPath);
            });
        }
    }
}
