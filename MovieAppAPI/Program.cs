using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieAppAPI.Services;
using MovieAppAPI.Services.Interfaces;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository;
using MovieListing.Repository.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ApplicationDBContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDBContextConnection"));
});
// Add services to the container.
builder.Services.AddHangfire(x => x.UseSqlServerStorage(string.Format(@"Server=(localdb)\\mssqllocaldb;Database=MovieListingApp;Trusted_Connection=True;MultipleActiveResultSets=true")));
builder.Services.AddHangfireServer();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//JWT CONFIG In SWAGGER GEN
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure Swagger to use JWT authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Here Enter your JWT Token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{ }
        }
    });
});

//Dependency Injection
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IYear, YearRepo>();
builder.Services.AddScoped<IGenre, GenreRepo>();
builder.Services.AddScoped<ICountries, CountryRepository>();
builder.Services.AddScoped<IMovies, MoviesRepo>();
builder.Services.AddTransient<ILoginRegister, LoginRegister>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    y.SaveToken = true;
    y.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//Adding Identity
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard("/mydashboard");
app.UseAuthentication(); //JWT Authentication
app.UseAuthorization();

app.MapControllers();

app.Run();



//--------------- JWT First Sample -----------------------


//builder.Services.AddSwaggerGen(options => { // Adding Swagger UI Authorization
//    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")", 
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });

//    options.OperationFilter<SecurityRequirementsOperationFilter>();
//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options=>
//{
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateActor = true,
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        RequireExpirationTime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
//        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value
//    };
//});