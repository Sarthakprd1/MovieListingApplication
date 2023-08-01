using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;
using System.Diagnostics.Metrics;

namespace MovieListing.Repository
{
    public class MoviesRepo : IMovies
    {
        private readonly ApplicationDBContext _dbContext;
        public MoviesRepo(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddMovies(Movies movies)
        {
            _dbContext.Movies.Add(movies);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteMovies(Movies movies)
        {
            if (movies == null)
            {
                return false;
            }
            else
            {
                _dbContext.Movies.Remove(movies);
                _dbContext.SaveChanges();
            }
            return true;
        }

        public List<Movies> GetAllMovies()
        {
            var data = _dbContext.Movies.Include(e => e.Year).Include(e => e.Genre).Include(e => e.Country).ToList();
            return data;
        }

        public Movies GetByID(int id)
        {
            var data = _dbContext.Movies.Find(id);
            return data;
        }

        public bool UpdateMovies(Movies movies)
        {
            //_dbContext.Entry(movies).State = EntityState.Modified;

            _dbContext.Movies.Update(movies);
            _dbContext.SaveChanges();
            return true;
        }

    }
}
