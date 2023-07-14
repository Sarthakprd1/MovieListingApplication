using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieListing.Models;

namespace MovieListing.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    //public ApplicationDBContext()
    //{
    //}

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);
    //    // Customize the ASP.NET Identity model and override the defaults if needed.
    //    // For example, you can rename the ASP.NET Identity table names and more.
    //    // Add your customizations after calling base.OnModelCreating(builder);
    //}
    public DbSet<Country> Countries { get; set; }
    public DbSet<Genre> Genre { get; set; } 
    public DbSet<Movies> Movies { get; set; }
    public DbSet<Year> Years { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
