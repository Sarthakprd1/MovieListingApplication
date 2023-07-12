using MovieListing.Models;

namespace MovieListing.Repository.Interfaces
{
    public interface IYear
    {
        bool AddYear(Year year);
        bool UpdateYear(Year year);
        bool DeleteYear(Year year);
        Year GetById (int id);  
        List<Year> GetYearList();
    }
}
