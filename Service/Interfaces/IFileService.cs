using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IFileService
    {
        Task SaveFile(IFormFile file, string filePath);
    }
}