using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CityOs.FileServer.Simple.WebApp
{
    public class Program
    {
        /// <summary>
        /// The main entry of the program
        /// </summary>
        /// <param name="args">The arguments</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Build the web host
        /// </summary>
        /// <param name="args">The arguments to build the web host</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
