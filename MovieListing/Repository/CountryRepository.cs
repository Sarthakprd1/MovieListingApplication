using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Repository
{
    public class CountryRepository : ICountries
    {
        private readonly ApplicationDBContext _dbContext;

        public CountryRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddCountries(Country country)
        {
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return true;
        }

        public bool EditCountry(Country country)
        {
            //var countrys = _dbContext.Countries.Where(x => x.Id == id).FirstOrDefault();
            //_dbContext.Countries.Remove(id);
            //_dbContext.Add(id);
            //_dbContext.Countries.Update(country);
            _dbContext.Entry(country).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }


        public bool DeleteCountries(Country country)
        {
            _dbContext.Countries.Remove(country);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Country> GetCountries()
        {
            var data = _dbContext.Countries.ToList();
            return data;
        }

        public Country GetByID(int id)
        {
            return _dbContext.Countries.Find(id);
        }
    }
}
