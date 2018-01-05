using System.Threading.Tasks;

namespace CityOs.FileServer.Core
{
    public interface IFileProvider
    {
        Task<string> Save(IFile file);

        
    }
}
