
using LagunaLink.Web.Data.Entities;
using LagunaLink.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LagunaLink.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

    }
}
