using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;
using System.Diagnostics.Metrics;

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
            _dBContext.Comments.Remove(comment);
            _dBContext.SaveChanges();
            return true;
        }

        public bool EditComments(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Comment GetById(int id)
        {
            return _dBContext.Comments.Find(id);
        }

        public List<Comment> GetComments()
        {
            var data = _dBContext.Comments.ToList();
            return data;
        }
    }
}
