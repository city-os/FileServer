using CityOs.FileServer.Distributed.Mvc.Extensions;
using CityOs.FileServer.Provider.FileSystem.Extensions;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CityOs.FileServer.Simple.WebApp
{
    public class Startup
    {
        /// <summary>
        /// Configure the application services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                });

            services.AddFileServer(options =>
            {
                options.UseFileSystem();
            });
        }

        /// <summary>
        /// Configure the request pipeline
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.UseFileServer2();

            app.UseMvcWithDefaultRoute();
        }
    }
}
