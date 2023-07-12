using Microsoft.VisualBasic;
using MovieListing.Areas.Identity.Data;
using System.Security.Policy;

namespace MovieListing.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? CommentDesc { get; set; }
        public int MovieId { get; set; }    
        public Movies? Movies { get; set;}
        public TimeSpan? TimeStamp { get; set; }
        //public ApplicationUser ApplicationUserUser { get; set; }
    }
}
