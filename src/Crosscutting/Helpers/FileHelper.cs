using System.IO;

namespace CityOs.FileServer.Crosscutting.Helpers
{
    public static class FileHelper
    {
        public static string BuildFileNameWithVersion(string fileName, int version)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);

           return $"{fileNameWithoutExtension}.{version}{extension}";
        }
    }
}
