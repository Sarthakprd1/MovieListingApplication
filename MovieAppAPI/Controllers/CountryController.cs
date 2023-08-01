using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        //Get Countries by ID
        [HttpGet("{id}")]
        public ActionResult<List<MovieDTO>> GetCountriesById(int id)
        {
            var getbyId = _ICountries.GetByID(id);
            if (getbyId == null)
            {
                return Ok("Country not found !");
            }
            return Ok(getbyId);

        }

        //ADD COUNTRY
        [HttpPost, Authorize(Roles = "User")]
        public ActionResult<List<CountryDTO>> AddCountry(CountryDTO country)
        {
            var mapcountry = _mapper.Map<Country>(country);
            var addcountry = _ICountries.AddCountries(mapcountry);
            if(addcountry == true)
            {
                return Ok("Country Added Successfuly.");
            }
            return Ok(addcountry); 
        }

        //UPDATE COUNTRY
        //[Authorize(Roles ="Admin"), Authorize]
        [HttpPut, Authorize(Roles = "Admin")]
        public ActionResult<List<CountryDTO>> UpdateCountry(Country country) 
        {
            var updatecountry = _ICountries.EditCountry(country);
            if (updatecountry == null)
            {
                return Ok("Country not found !");
            }
            return Ok(updatecountry); 
        }

        //DELETE COUNTRY
        //[Authorize(Roles = "Admin"), Authorize]
        [HttpDelete("{id}"),Authorize(Roles = "Admin")]
        public ActionResult DeleteCountry(int id)
        {
            var findid = _ICountries.GetByID(id);   
            var deletecountry = _ICountries.DeleteCountries(findid);
            if (findid != null)
            {
                return Ok("Country Deleted Successfully.");
            }
            else if(findid == null)
            {
                return Ok("Associated Country ID Not Found !");
            }
            return Ok(deletecountry); 
        }

    }
}

