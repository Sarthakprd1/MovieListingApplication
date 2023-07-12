using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace MovieListing.Areas.Identity.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
    }
}


