using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CityOs.FileServer.Distributed.Mvc.Security
{
    internal class ReadAuthorizationHandler : AuthorizationHandler<ReadAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadAuthorizationRequirement requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
