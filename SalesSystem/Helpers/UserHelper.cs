using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserHelper(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public IdentityUser GetUserByEmail(string email)
        {
            return _userManager.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}
