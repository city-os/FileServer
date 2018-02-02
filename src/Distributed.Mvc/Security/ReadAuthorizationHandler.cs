using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CityOs.FileServer.Distributed.Mvc.Security
{
    internal class ReadAuthorizationHandler : AuthorizationHandler<ReadAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadAuthorizationRequirement requirement)
        {
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                var hasWriteClaim = context.User.HasClaim(c => c.Type == "scope" && (c.Value == "fileserver:read" || c.Value == "fileserver:read_write"));

                if (hasWriteClaim)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
