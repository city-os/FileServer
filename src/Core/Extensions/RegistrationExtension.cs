using Microsoft.Extensions.DependencyInjection;

namespace CityOs.FileServer.Core
{
    public static class RegistrationExtension
    {
        public static IMvcBuilder AddFileServer(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(DocumentController).Assembly);
        }
    }
}
