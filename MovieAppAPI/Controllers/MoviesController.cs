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
            _IMovies= imovies;  
            _mapper= mapper;
        }

        [HttpGet]
        public ActionResult<List<MovieDTO>> GetMoviesList() 
        {

            var getmovieslist = _IMovies.GetAllMovies();
            var result = getmovieslist.Select(x => _mapper.Map<MovieDTO>(x)).ToList();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<List<MovieDTO>> AddMovies(MovieDTO movies)
        {
            var mapmovies = _mapper.Map<Movies>(movies);
            var addmovies = _IMovies.AddMovies(mapmovies);

            return Ok(addmovies);
        }

        [HttpPut]
        public ActionResult<List<MovieDTO>> UpdateMovie(MovieDTO movie) 
        {
            var mapmovies = _mapper.Map<Movies>(movie);
            var updatemovies = _IMovies.UpdateMovies(mapmovies);        
            return Ok(updatemovies);
        }

        [HttpDelete]
        public ActionResult DeleteMovie(int id)
        {
            var mapmovies = _mapper.Map<Movies>(id);
            var deletemovies =_IMovies.DeleteMovies(mapmovies); 
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