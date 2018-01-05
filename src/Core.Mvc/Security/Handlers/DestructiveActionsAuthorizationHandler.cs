using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CityOs.FileServer.Core.Mvc.Security
{
    internal class DestructiveActionsAuthorizationHandler : AuthorizationHandler<DestructiveActionsRequirement>
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DestructiveActionsRequirement requirement)
        {
            var userClaims = context.User.Claims.ToList();

            var organizationClaim = userClaims.FirstOrDefault(c => c.Type == FileClaimTypes.Organization);

            if (organizationClaim == null)
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
