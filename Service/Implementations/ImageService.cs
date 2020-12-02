using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ImageService : IImageService
    {
        public async Task SaveImage(IFormFile file, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // image postprocessing maybe in future...
        }
    }
}