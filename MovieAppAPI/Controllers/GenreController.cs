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
            return Ok(genre);
        }

        //AddGenre
        [HttpPost]
        public ActionResult<List<GenreDTO>> AddGenre(GenreDTO genre)
        {
            var genres = _mapper.Map<Genre>(genre);
            var addgenre = _IGenre.AddGenre(genres);
            return Ok(addgenre);
        }

        [HttpPut]
        public ActionResult<List<GenreDTO>> UpdateGenre(Genre genre)
        {
            var updategenre = _IGenre.UpdateGenre(genre);
            return Ok(updategenre);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<GenreDTO>> DeleteGenre(int id)
        {
            var findid = _IGenre.GetByID(id);
            var deletegenre = _IGenre.DeleteGenre(findid);
            return Ok(deletegenre);
        }

    }
}
