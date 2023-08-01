using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Repository
{
    public class GenreRepo : IGenre
    {
        private readonly ApplicationDBContext _dbContext; //Calling DB
        public GenreRepo(ApplicationDBContext dbContext) //Making Object for Database in repo which ends the direct connection of controller with DB.
        {
            _dbContext = dbContext;
        }

        public bool AddGenre(Genre genre)
        {
            _dbContext.Genre.Add(genre);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteGenre(Genre genre)
        {
            if (genre == null)
            {
                return false;
            }
            else
            {
                _dbContext.Genre.Remove(genre);
                _dbContext.SaveChanges();
            }
           
            return true;
        }

        public List<Genre> GetAllGenre()
        {
            var data = _dbContext.Genre.ToList();
            return data;
        }

        public Genre GetByID(int id)
        {
            return _dbContext.Genre.Find(id);
        }

        public bool UpdateGenre(Genre genre)
        {
            _dbContext.Entry(genre).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
