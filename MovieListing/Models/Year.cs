using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace MovieListing.Models
{
    public class Year
    {
        [Key]
        public int Id { get; set; }
        public string Years { get; set; }
    }
}
