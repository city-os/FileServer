using CityOs.FileServer.Distributed.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CityOs.FileServer.Distributed.Mvc.Extensions
{
    public static class FileServerApplicationBuilderExtensions
    {
        public static void UseFileServer2(this IApplicationBuilder app)
        {
            app.UseMiddleware<FileServerErrorHandlingMiddleware>();
        }
    }
}
