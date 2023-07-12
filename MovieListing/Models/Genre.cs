using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieListing.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        //[ForeignKey("Movies")]
        //public int MovieRefId { get; set; }
        //public Movies? Movie { get; set; }
    }
}
