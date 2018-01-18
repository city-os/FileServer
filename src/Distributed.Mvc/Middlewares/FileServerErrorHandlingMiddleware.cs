using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CityOs.FileServer.Distributed.Mvc.Middleware
{
    internal class FileServerErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public FileServerErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.HasValue || !context.Request.Path.Value.Contains("api/documents/"))
            {
                await _next.Invoke(context);
                return;
            }

            try
            {
                await _next.Invoke(context);
            }
            //catch (System.Exception e)
            //{
            //    context.Response.StatusCode = 500;
            //    await context.Response.WriteAsync(e.Message);
            //}
            finally { }
        }
    }
}
