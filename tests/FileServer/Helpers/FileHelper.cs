using System.IO;
using System.Reflection;

namespace CityOs.FileServer.Tests.Helpers
{
    internal static class FileHelper
    {
        /// <summary>
        /// Extract a stream from the data folder
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        public static Stream GetEmbeddedStream(string fileName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resource = string.Format("CityOs.FileServer.Tests.Data.{0}", fileName);

            var stream = executingAssembly.GetManifestResourceStream(resource);

            return stream;
        }
    }
}
