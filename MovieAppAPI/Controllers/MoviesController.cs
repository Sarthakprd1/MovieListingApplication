using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.DTO;
using MovieListing.Areas.Identity.Data;
using MovieListing.Controllers;
using MovieListing.Models;
using MovieListing.Models.StoreDB;
using MovieListing.Repository.Interfaces;

namespace MovieAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {

        private readonly IMovies _IMovies;
        private readonly IMapper _mapper;
        public MoviesController(IMovies imovies, IMapper mapper)
        {
            _IMovies = imovies;
            _mapper = mapper;
        }

        //Get All Movies List.
        [HttpGet]
        public ActionResult<List<MovieDTO>> GetMoviesList()
        {

            var getmovieslist = _IMovies.GetAllMovies();
            var result = getmovieslist.Select(x => _mapper.Map<MovieDTO>(x)).ToList();

            return Ok(result);
        }

        //Get Movies By ID
        [HttpGet("{id}")]
        public ActionResult<List<MovieDTO>> GetMoviesByID(int id)
        {
            var findMovieId = _IMovies.GetByID(id);
            if (findMovieId == null)
            {
                return BadRequest("Movie ID Not Found !");
            }
            return Ok(findMovieId);
        }
        //Add New Movies
        [HttpPost]
        public ActionResult<List<MovieCreateDTO>> AddMovies(MovieCreateDTO movies)
        {
            var mapmovies = _mapper.Map<Movies>(movies);
            var addmovies = _IMovies.AddMovies(mapmovies);
            if(addmovies == true)
            {
                return Ok("Movie Added Successfully.");
            }
            else if(addmovies == false)
            {
                return BadRequest("Failed to Add New Movies");
            }

            return Ok(addmovies);
        }

        //Update Movies
        [HttpPut]
        public ActionResult<List<MovieCreateDTO>> UpdateMovie(MovieDTO movies)
        {
            var mapmovies = _mapper.Map<Movies>(movies);
            var updatemovies = _IMovies.UpdateMovies(mapmovies);
            return Ok(updatemovies);

            //var findbyid = _IMovies.GetByID(movies.Id);
            //if (findbyid == null)
            //{
            //    return BadRequest("Movie Not Found");
            //}
            //var mapmovies = _mapper.Map<Movies>(id);
            //var updatemovies = _IMovies.UpdateMovies(mapmovies);        
        }

        //Delete Movies By ID.
        [HttpDelete("{id}")]
        public ActionResult<List<MovieDTO>> DeleteMovie(int id)
        {
            //var findid = _mapper.Map<Movies>(id);
            var findId = _IMovies.GetByID(id);
            var deletemovies = _IMovies.DeleteMovies(findId);
            if (findId == null)
            {
                return BadRequest("Movie ID Not Found !");
            }
            else if (deletemovies == true)
            {
                return Ok("Movie Deleted Successfully !");
            }
            return Ok(deletemovies);
        }

        //----------------------------------REFERENCE PART--------------------------------------------------------------//

        //[HttpGet] //Get all Movies
        ////User Requests for data
        //public IActionResult Get()
        //{
        //    //return Ok(heroes); // returns from above list
        //    return Ok(_dbcontext.Movies.ToList());
        //}

        ////Get method for single SuperHero By Id
        ////User Requests for data
        //[HttpGet("{id}")] // Passing Required Field according to the parameter
        //public IActionResult Get(int id) // You can change the parameter and its data type and pass it to the required field.
        //{
        //    //var hero = heroes.Find(x => x.Id == id);
        //    var hero = _dbcontext.Movies.Find(id);
        //    if (hero == null)
        //    {
        //        return BadRequest("Hero Not Found!");
        //    }
        //    return Ok(hero);
        //}

        //[HttpPut] //It Updates the data 
        //public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        //{
        //    var dbHero = await _context.SuperHeroes.FindAsync(request.Id); //Checks heroes object and request Object by Id and stores in hero object //Lambda expression 
        //    if (dbHero == null) //If hero variable which is Id / int datatype is null throws exception
        //    {
        //        return BadRequest("Hero Not Found!");
        //    }

        //    dbHero.Name = request.Name; //Updates hero with request objects
        //    dbHero.FirstName = request.FirstName;
        //    dbHero.LastName = request.LastName;
        //    dbHero.Place = request.Place;

        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.SuperHeroes.ToListAsync());
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<List<SuperHero>>> Delete(int id) // You can change the parameter and its data type and pass it to the required field.
        //{
        //    var dbhero = await _context.SuperHeroes.FindAsync(id);
        //    if (dbhero == null)
        //    {
        //        return BadRequest("Hero Not Found!");
        //    }
        //    _context.SuperHeroes.Remove(dbhero);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.SuperHeroes.ToListAsync());
        //}
    }
}