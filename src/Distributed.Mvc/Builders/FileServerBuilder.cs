﻿using AutoMapper;
using CityOs.FileServer.AppService;
using CityOs.FileServer.AppService.Adapters;
using CityOs.FileServer.Distributed.Mvc.Security;
using CityOs.FileServer.Domain.Contracts;
using CityOs.FileServer.Domain.Services;
using CityOs.FileServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CityOs.FileServer.Distributed.Mvc
{
    public class FileServerBuilder : IFileServerBuilder
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
        public void TryAddCoreServices()
        {
            _services.AddScoped<IDocumentAppService, DocumentAppService>();
            _services.AddScoped<IImageAppService, ImageAppService>();

            _services.AddScoped<IFileDomainService, FileDomainService>();
            _services.AddScoped<IImageDomainService, ImageDomainService>();

            _services.AddScoped<IDocumentRepository, DocumentRepository>();
            _services.AddScoped<IImageRepository, ImageRepository>();

            _services.AddAuthorization(a => a.AddPolicy("ReadDocument", builder => builder.AddRequirements(new ReadAuthorizationRequirement())));
            _services.AddAuthorization(a => a.AddPolicy("WriteDocument", builder => builder.AddRequirements(new WriteAuthorizationRequirement())));

            _services.AddSingleton<IAuthorizationHandler, ReadAuthorizationHandler>();
            _services.AddSingleton<IAuthorizationHandler, WriteAuthorizationHandler>();

            _services.AddAutoMapper(typeof(FileServerProfile).Assembly);
        }

        /// <summary>
        /// Try add singleton to the service collection
        /// </summary>
        /// <typeparam name="TInterface">The interface</typeparam>
        /// <typeparam name="TImplementation">The implementation of the interface</typeparam>
        public void TryAddSingleton<TInterface, TImplementation>() 
            where TImplementation : class, TInterface
            where TInterface : class
        {
            _services.AddSingleton<TInterface, TImplementation>();
        }

        /// <summary>
        /// Try add singleton to the service collection
        /// </summary>
        /// <typeparam name="TInterface">The interface</typeparam>
        /// <typeparam name="TImplementation">The implementation</typeparam>
        /// <param name="implementationFactory">The factory of the implementation</param>
        public void TryAddSingleton<TInterface, TImplementation>(Func<IServiceProvider, TImplementation> implementationFactory)
            where TImplementation : class, TInterface
            where TInterface : class
        {
            _services.AddSingleton<TInterface, TImplementation>(implementationFactory);
        }
    }
}
