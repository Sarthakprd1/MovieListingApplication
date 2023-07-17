using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovieListing.Areas.Identity.Data;

namespace MovieListing.Models.StoreDB
{
    

    public class YearDb
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        public YearDb(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("ApplicationDBContextConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        //declare connection string


        ////Return List of all Years
        //public List<Year> ListAll()
        //{
        //    List<Year> list = new List<Year>();
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        SqlCommand com = new SqlCommand("SelectYears", con);
        //        com.Par
        //    }
        //        return new List<Year>();
        //}

        public int AddDB(Year years)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand com = new SqlCommand("InsertYears", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", years.Id);
                com.Parameters.AddWithValue("@Years", years.Years);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}

