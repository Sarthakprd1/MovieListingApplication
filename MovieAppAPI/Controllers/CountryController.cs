using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.DTO;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountries _ICountries;
        private readonly IMapper _mapper;
        public CountryController (IMapper mapper, ICountries countries)
        {
            _mapper = mapper;
            _ICountries = countries;
        }

        //GET COUNTRY LIST
        [HttpGet]
        public ActionResult<List<CountryDTO>> GetCountryList ()
        {
            var countryList = _ICountries.GetCountries();
            return Ok(countryList);
        }

        //ADD COUNTRY
        [HttpPost]
        public ActionResult<List<CountryDTO>> AddCountry(CountryDTO country)
        {
            var mapcountry = _mapper.Map<Country>(country);
            var addcountry = _ICountries.AddCountries(mapcountry);
            return Ok(addcountry); 
        }

        //UPDATE COUNTRY
        [HttpPut]
        public ActionResult<List<CountryDTO>> UpdateCountry(Country country) 
        {
            var updatecountry = _ICountries.EditCountry(country);   
            return Ok(updatecountry); 
        }

        //DELETE COUNTRY
        [HttpDelete]
        public ActionResult DeleteCountry(int id)
        {
            var findid = _ICountries.GetByID(id);   
            var deletecountry = _ICountries.DeleteCountries(findid);    
            return Ok(deletecountry); 
        }

    }
}

