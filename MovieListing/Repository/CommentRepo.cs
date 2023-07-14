using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace MovieListing.Repository
{

    public class CommentRepo : IComment
    {
        private readonly ApplicationDBContext _dBContext;

        public CommentRepo(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public bool AddComments(Comment comment)
        {
            _dBContext.Comments.Add(comment);
            _dBContext.SaveChanges();
            return true;
        }


        public bool DeleteComments(Comment comment)
        {
            var comments = _dBContext.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
            _dBContext.Comments.Remove(comments);
            _dBContext.SaveChanges();
            return true;
        }

        public bool EditComments(Comment comment)
        {
            

            _dBContext.Comments.Update(comment);    
            _dBContext.SaveChanges();
            return true;
        }

        public Comment GetById(int id)
        {
            return _dBContext.Comments.Find(id);
        }

        public List<Comment> GetComments(int movieid)
        {
            var data = _dBContext.Comments.Include(e=>e.IdentityUser).Where(c=>c.MovieId==movieid).ToList();
            return data;
        }
    }
}
