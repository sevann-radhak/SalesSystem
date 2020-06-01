using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SalesSystem.Helpers
{
    public interface IUserHelper
    {
        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role);
        Task<IdentityResult> CreateAsync(IdentityUser user, string password);
        IdentityUser GetUserByEmail(string email);
    }
}
