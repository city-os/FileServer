using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CityOs.FileServer.Crosscutting.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Convert a file name into a unique identifier name
        /// </summary>
        /// <param name="fileName">The file name to convert</param>
        /// <returns></returns>
        public static string GetUniqueFileName()
        {
            var uniqueFileName = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            
            return uniqueFileName;
        }
    }
}
