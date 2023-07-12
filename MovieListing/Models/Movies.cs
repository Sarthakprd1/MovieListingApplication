using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieListing.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        
        [Display(Name = "Insert an Image")]
        public string MoviePhoto { get; set; }

        [Required]
        [ForeignKey("Year")]
        public int YearId { get; set; }
        public Year Year { get; set; }

        [Required]
        [ForeignKey("Genre")]
        public int GenreRefId { get; set; }
        public Genre Genre { get; set; }

        [Required]
        [ForeignKey("Country")]
        public int CountryRefId { get; set; }
        public Country Country { get; set; }

        public List<Comment> Comments { get; set; } 


    }
}
