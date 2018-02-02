using CityOs.FileServer.Distributed.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CityOs.FileServer.Distributed.Mvc.Extensions
{
    public static class FileServerApplicationBuilderExtensions
    {
        public static void UseSimpleFileServer(this IApplicationBuilder app)
        {
            app.UseMiddleware<FileServerErrorHandlingMiddleware>();
        }
    }
}
