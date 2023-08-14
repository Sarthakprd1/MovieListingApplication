using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MovieAppAPI.DTO
{
    public class RegisterDTO
    {
        //public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}
