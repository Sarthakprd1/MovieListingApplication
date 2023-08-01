using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using MovieAppAPI.Services.Interfaces;
using MovieListing.Areas.Identity.Pages.Account;

namespace MovieAppAPI.Services
{
    public class LoginRegister : ILoginRegister
    {
        private readonly UserManager<IdentityUser> _userManager;
        public LoginRegister(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Login(IdentityUser user)
        {
           var identityUser = await _userManager.FindByEmailAsync(user.Email);    
           if(identityUser is null)
           {
                return false;
           }
            return await _userManager.CheckPasswordAsync(identityUser, user.PasswordHash);

        }

        public async Task<bool> RegisterUser(IdentityUser user)
        {
            var identityUser = new IdentityUser
            {
                UserName= user.UserName,
                Email= user.Email
            };

            var result = await _userManager.CreateAsync(identityUser, user.PasswordHash);
            return result.Succeeded;
        }
    }
}
