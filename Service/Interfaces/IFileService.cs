using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IFileService
    {
        Task SaveFile(IFormFile file, string filePath);

        Task<string> UploadFile(IFormFile file, string uploadDir);

        void DeleteFile(string fileName, string uploadDir);
    }
}