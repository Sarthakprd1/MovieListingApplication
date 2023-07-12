using System.ComponentModel.DataAnnotations;

namespace MovieListing.Models
{
    public class Country
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public static bool IsDeleted { get; set; } = false;
    }
}
