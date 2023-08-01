using Microsoft.AspNetCore.Identity;

namespace MovieAppAPI.Services.Interfaces
{
    public interface ILoginRegister
    {
        
        Task<bool> Login(IdentityUser user);
        
        Task<bool> RegisterUser(IdentityUser user);
    }
}
