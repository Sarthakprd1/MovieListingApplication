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
    public class GenreController : ControllerBase
    {
        // Here stores the implementation of CRUD of Genre Model.
        private readonly IGenre _IGenre;
        private readonly IMapper _mapper;

        public GenreController(IGenre igenre, IMapper mapper)
        {
            _IGenre = igenre;
            _mapper = mapper;
        }

        //GetGenreList
        [HttpGet]
        public ActionResult<List<GenreDTO>> GetGenre()
        {
            var genre = _IGenre.GetAllGenre();
            if (genre == null)
            {
                return BadRequest("Genre Not Found !");
            }

            return Ok(genre);
        }

        //GetByGenreId
        [HttpGet("{id}")]
        public ActionResult<List<GenreDTO>> GetGenreId(int id) 
        {
            var genre = _IGenre.GetByID(id);
            if (genre == null)
            {
                return BadRequest("Associated Genre ID Not Found !");
            }
            return Ok(genre);
        }


        //AddGenre
        [HttpPost]
        public ActionResult<List<GenreDTO>> AddGenre(GenreDTO genre)
        {
            var genres = _mapper.Map<Genre>(genre);
            var addgenre = _IGenre.AddGenre(genres);
            if (addgenre == true)
            {
                return Ok("Successfully Added Genre");
            }
            return Ok(addgenre);
        }

        [HttpPut]
        public ActionResult<List<GenreDTO>> UpdateGenre(Genre genre)
        {
            var updategenre = _IGenre.UpdateGenre(genre);
            if (updategenre == true)
            {
                return Ok("Genre Updated Successfully !");
            }
            return Ok(updategenre);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<GenreDTO>> DeleteGenre(int id)
        {
            var findid = _IGenre.GetByID(id);
            var deletegenre = _IGenre.DeleteGenre(findid);
            if (findid ==null)
            {
                return BadRequest("Genre ID Not Found, Deletion Failed !");
            }
            else if(deletegenre == true)
            {
                return Ok("Genre Deleted Succesfully. <3");
            }
            return Ok(deletegenre);
        }

    }
}
