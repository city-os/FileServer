using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.WebApp
{
    public class Configuration
    {
        /// <summary>
        /// Gets the API resources.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("api", "Api");
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                ClientId = "server",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes =
                {
                    "api"
                }
            };
        }
    }
}
