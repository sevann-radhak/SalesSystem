using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace SalesSystem.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<byte[]> ByteAvatarImageAsync(
            IFormFile AvatarImage, 
            IWebHostEnvironment enviroment,
            string path)
        {
            if (AvatarImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await AvatarImage.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else
            {
                return File.ReadAllBytes($"{enviroment.ContentRootPath}/wwwroot/{path}");
            }
        }
    }
}
