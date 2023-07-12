using MovieListing.Models;

namespace MovieListing.Repository.Interfaces
{
    public interface IMovies
    {
        bool AddMovies(Movies movies);
        bool UpdateMovies(Movies movies);
        bool DeleteMovies(Movies movies);
        Movies GetByID(int id);
        List<Movies> GetAllMovies();
    }
}
