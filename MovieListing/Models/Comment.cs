using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using MovieListing.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace MovieListing.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentDesc { get; set; }

        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        public Movies Movies { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }


        public DateTime TimeStamp { get; set; }
        
    }
}
