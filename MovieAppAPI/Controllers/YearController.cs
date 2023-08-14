
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.DTO;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        //Here Stores all the methods of Year CRUD Implementations.

        private readonly IMapper _mapper;
        private readonly IYear _Iyear;

        public YearController(IMapper mapper, IYear iyear)
        {
            _mapper = mapper;
            _Iyear = iyear;
        }

        //Get All Years.
        [HttpGet]
        public ActionResult<List<YearDTO>> GetYear()
        {
            var years = _Iyear.GetYearList();
            //var result = years.Select(x => _mapper.Map<YearDTO>(x)).ToList();
            return Ok(years);
        }

        //GetById
        [HttpGet("{id}")]
        public ActionResult<List<YearDTO>> GetById(int id)
        {
            var findYearId = _Iyear.GetById(id);
            if (findYearId == null)
            {
                return Ok("Year ID Not Found !");
            }
            return Ok(findYearId);
        }

        //Add Years.
        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult<List<YearDTO>> AddYear(YearDTO year)
        {
            var years = _mapper.Map<Year>(year);
            var addyear = _Iyear.AddDb(years);
            if(addyear == true)
            {
                return Ok("Year Added Successfully !");
            }
            return Ok(addyear);
        }

        //Update Years.
        [HttpPut, Authorize(Roles = "Admin")]
        public ActionResult<List<YearDTO>> UpdateYear(Year year)
        {
            //var years = _mapper.Map<Year>(year);

            var updateYear = _Iyear.UpdateDb(year);
            if (updateYear == true)
            {
                return Ok("Years Updated Successfully !");
            }
            else if (updateYear == false)
            {
                return Ok("Failed While Updating Years.");
            }

            return Ok(updateYear);
        }

        //Delete Years By ID
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public ActionResult<List<YearDTO>> DeleteYear(int id)
        {
            //var years = _mapper.Map<Year>(id);
            var findbyId = _Iyear.GetById(id);
            var deleteyear = _Iyear.DeleteYear(findbyId);
            if (findbyId == null)
            {
                return BadRequest("Associated Year ID Not Found !");
            }
            else if (findbyId != null)
            {
                return Ok("Year Successfully Deleted");
            }
            return Ok(deleteyear);
        }
    }
}
