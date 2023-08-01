using Microsoft.AspNetCore.Identity;

namespace MovieAppAPI
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
