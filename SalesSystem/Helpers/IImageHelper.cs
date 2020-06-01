using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SalesSystem.Helpers
{
    public interface IImageHelper
    {
        Task<byte[]> ByteAvatarImageAsync(IFormFile AvatarImage, IWebHostEnvironment enviroment, string path);
    }
}
