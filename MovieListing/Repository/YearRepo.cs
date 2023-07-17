 using Humanizer.Localisation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Models.StoreDB;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Repository
{
    public class YearRepo : IYear
    {
        private readonly ApplicationDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public YearRepo(ApplicationDBContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("ApplicationDBContextConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public bool AddDb(Year year)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand com = new SqlCommand("InsertYear", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("@Id", year.Id);
                com.Parameters.AddWithValue("@Years", year.Years);
                i = com.ExecuteNonQuery();
            }
            return true;
        }

        public bool UpdateDb(Year year)
        {
            int i;
            using (SqlConnection update = new SqlConnection(ConnectionString))
            {
                update.Open();

                SqlCommand com = new SqlCommand("UpdateYear", update);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", year.Id);
                com.Parameters.AddWithValue("@Years", year.Years);
                i = com.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteDb(Year year)
        {
            int i;
            using (SqlConnection delete = new SqlConnection(ConnectionString))
            {
                delete.Open();

                SqlCommand com = new SqlCommand("DeleteYear", delete);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", year.Id);
                i = com.ExecuteNonQuery();
            }
            return true;
        }

        public bool AddYear(Year year)
        {
        //    _dbcontext.Years.Add(year);
        //    _dbcontext.SaveChanges();
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
