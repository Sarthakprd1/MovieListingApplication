using AutoMapper;
using MovieAppAPI.DTO;
using MovieListing.Models;

namespace MovieAppAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Year, YearDTO>();
            CreateMap<YearDTO,Year>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();
            CreateMap<CountryDTO, Country>();   
            CreateMap<Country, CountryDTO>();
            CreateMap<Movies, MovieDTO>();
            CreateMap<MovieDTO, Movies>();    
            
        }
    }
}
