using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieAppAPI.DTO;
using MovieListing.Models;

namespace MovieAppAPI.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Year, YearDTO>();
            CreateMap<YearDTO, Year>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();
            CreateMap<CountryDTO, Country>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Movies, MovieDTO>();
            CreateMap<MovieDTO, Movies>();
            CreateMap<MovieCreateDTO, Movies>();
            CreateMap<RegisterDTO, IdentityUser>();
            CreateMap<IdentityUser, RegisterDTO>();
            CreateMap<LoginDTO, IdentityUser>();
            CreateMap<IdentityUser, LoginDTO>();
        }
    }
}
