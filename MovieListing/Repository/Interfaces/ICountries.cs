using MovieListing.Models;

namespace MovieListing.Repository.Interfaces
{
    public interface ICountries
    {
        bool AddCountries(Country country);
        bool EditCountry(Country country);

        bool DeleteCountries(Country country);

        Country GetByID(int id);

        List<Country> GetCountries();

    }
}
