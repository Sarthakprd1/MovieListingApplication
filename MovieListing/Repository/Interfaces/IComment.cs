using MovieListing.Models;

namespace MovieListing.Repository.Interfaces
{
    public interface IComment
    {
        bool AddComments(Comment comment);
        bool DeleteComments(Comment comment);
        bool EditComments(Comment comment);
        Comment GetById(int id);
        List<Comment> GetComments();
    }
}
