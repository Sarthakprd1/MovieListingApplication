using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace MovieListing.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

    }

}
