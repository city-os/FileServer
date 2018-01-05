using CityOs.FileServer.Core.Mvc.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CityOs.FileServer.Core.Mvc.Extensions
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
            return AddFileServer(services, null);
        }

        /// <summary>
        /// Adds the file server.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="optionsAction">The options action.</param>
        /// <returns></returns>
        public static IFileServerBuilder AddFileServer(this IServiceCollection services, Action<FileServerOptions> optionsAction)
        {
            var fileServerBuilder = new FileServerBuilder();

            services.AddAuthorization(a => a.AddPolicy("DestructiveActions", builder => builder.AddRequirements(new DestructiveActionsRequirement())));

            services.AddSingleton<IAuthorizationHandler, DestructiveActionsAuthorizationHandler>();

            return fileServerBuilder;
        }
    }
}
