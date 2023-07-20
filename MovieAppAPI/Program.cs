using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository;
using MovieListing.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ApplicationDBContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDBContextConnection"));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IYear, YearRepo>();
builder.Services.AddScoped<IGenre, GenreRepo>();
builder.Services.AddScoped<ICountries, CountryRepository>();
builder.Services.AddScoped<IMovies, MoviesRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
