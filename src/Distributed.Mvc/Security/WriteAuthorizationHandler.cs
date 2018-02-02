using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CityOs.FileServer.Distributed.Mvc.Security
{
    internal class WriteAuthorizationHandler : AuthorizationHandler<WriteAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WriteAuthorizationRequirement requirement)
        {
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                var hasWriteClaim = context.User.HasClaim(c => c.Type == "scope" && c.Value == "fileserver:read_write");

                if (hasWriteClaim)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
