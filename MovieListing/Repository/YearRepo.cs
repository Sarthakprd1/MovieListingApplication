 using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Repository
{
    public class YearRepo : IYear
    {
        private readonly ApplicationDBContext _dbcontext;

        public YearRepo(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            
        }

        public bool AddYear(Year year)
        {
            _dbcontext.Years.Add(year);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool DeleteYear(Year year)
        {
            _dbcontext.Years.Remove(year);
            _dbcontext.SaveChanges();
            return true;
        }

        public Year GetById(int id)
        {
            return _dbcontext.Years.Find(id);
        }

        public List<Year> GetYearList()
        {
            var data = _dbcontext.Years.ToList();
            return data;
        }

        public bool UpdateYear(Year year)
        {
            _dbcontext.Entry(year).State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return true;
        }
    }
}
