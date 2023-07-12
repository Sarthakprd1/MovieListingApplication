using MovieListing.Models;

namespace MovieListing.Repository.Interfaces
{
    public interface IGenre
    {
        bool AddGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        Genre GetByID(int id);
        List<Genre> GetAllGenre();
    }
}
