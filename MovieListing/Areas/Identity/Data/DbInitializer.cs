using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Areas.Identity.Data
{
    public class DbInitializer : IDbInitalizer
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDBContext dBContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dBContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Initalizer()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!_roleManager.RoleExistsAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString())).GetAwaiter().GetResult();
            }

            var x = new IdentityUser();
            var user = new IdentityUser
            {
                PhoneNumber = "9855066701",
                PhoneNumberConfirmed = true,
                Email = "movies@admin.com",
                UserName = "movies@admin.com",
                NormalizedEmail = "MOVIES@ADMIN.COM",
                NormalizedUserName = "MOVIES@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            var userManager = _userManager.CreateAsync(user, "Admin@123").GetAwaiter().GetResult();
            var result = _dbContext.Users.FirstOrDefault(a => a.Email == "movies@admin.com");
            _userManager.AddToRoleAsync(user, UserRole.Admin.ToString()).GetAwaiter().GetResult();
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
