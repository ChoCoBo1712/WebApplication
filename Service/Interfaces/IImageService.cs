using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IImageService
    {
        Task SaveImage(IFormFile file, string filePath);
    }
}