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
            yield return new ApiResource("fileserver", "The file server api resource")
            {
                Scopes =
                {
                    new Scope("fileserver:read"),
                    new Scope("fileserver:read_write")
                }
            };
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientId = "server",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes =
                {
                    "fileserver:read",
                    "fileserver:read_write"
                }
            };
        }
    }
}
