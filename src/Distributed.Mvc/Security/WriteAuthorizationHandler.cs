using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CityOs.FileServer.Distributed.Mvc.Security
{
    internal class WriteAuthorizationHandler : AuthorizationHandler<WriteAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WriteAuthorizationRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}
