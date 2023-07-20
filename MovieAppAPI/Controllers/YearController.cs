
using AutoMapper;

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

        [HttpGet]
        public ActionResult<List<YearDTO>> GetYear()
        {
            var years = _Iyear.GetYearList();
            //var result = years.Select(x => _mapper.Map<YearDTO>(x)).ToList();
            return Ok(years);
        }

        [HttpPost]
        public ActionResult<List<YearDTO>> AddYear(YearDTO year)
        {
            var years = _mapper.Map<Year>(year);
            var addyear =  _Iyear.AddDb(years);

            return Ok(addyear);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<YearDTO>> DeleteYear(int id)
        {
            //var years = _mapper.Map<Year>(id);
            var find = _Iyear.GetById(id);
            var deleteyear = _Iyear.DeleteDb(find);

            return Ok(deleteyear);
        }

        [HttpPut]
        public ActionResult<List<YearDTO>> UpdateYear(Year year)
        {
            //var years = _mapper.Map<Year>(year);

            var updateYear = _Iyear.UpdateDb(year);

            return Ok(updateYear);
        }


    }
}
