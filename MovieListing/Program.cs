using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Repository;
using MovieListing.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDBContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDbInitalizer, DbInitializer>();
builder.Services.AddScoped<ICountries, CountryRepository>();
builder.Services.AddScoped<IGenre, GenreRepo>();
builder.Services.AddScoped<IYear, YearRepo>();
builder.Services.AddScoped<IMovies, MoviesRepo>();
builder.Services.AddScoped<IComment, CommentRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
//using(var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    var roles = new[] { "Admin", "User" };
//    foreach(var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role)
//            await roleManager.CreateAsync(new IdentityRole(role));
//    }
//}
SeedDatabase();
app.Run();

void SeedDatabase()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitalizer>();
        dbInitializer.Initalizer();
    }
}