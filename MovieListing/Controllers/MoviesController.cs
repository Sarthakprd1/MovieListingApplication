using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;
using System.Diagnostics.Metrics;

namespace MovieListing.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IComment _IComment;
        private readonly IGenre _IGenre;
        private readonly ICountries _ICountries;
        private readonly IYear _Iyear;
        private readonly IMovies _IMovies;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MoviesController(IMovies movies, IWebHostEnvironment webHostEnvironment, IYear year, IGenre genre, ICountries countries, IComment comment)
        {
            _IMovies = movies;
            _webHostEnvironment = webHostEnvironment;
            _Iyear = year;
            _IGenre = genre;
            _ICountries = countries;
            _IComment = comment;
        }
        //Manual Pagination

        //public IActionResult Index(int pg = 1)
        //{

        //    //var data = _IMovies.GetAllMovies();
        //    //const int pageSize = 5;
        //    //if (pg < 1)

        //    //    pg = 1;
        //    //int recsCount = data.Count();
        //    //var pager = new Pager(recsCount, pg, pageSize);
        //    //int recSkip = (pg - 1) * pageSize;
        //    //var datas = data.Skip(recSkip).Take(pageSize).ToList();
        //    //this.ViewBag.Pager = pager;

        //    return View(data);
        //}

        public IActionResult Index()
        {
            var data = _IMovies.GetAllMovies();
            return View(data);
        }

        //CREATE
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var data = new Movies(); //Create New Movies 

            //Drop down list for years
            var yearList = new SelectList(_Iyear.GetYearList(), "Id", "Years");
            ViewData["YearId"] = yearList;

            //Drop down list for Genre
            var genreList = new SelectList(_IGenre.GetAllGenre(), "Id", "Name");
            ViewData["GenreId"] = genreList;

            //Drop down list for Country
            var countryList = new SelectList(_ICountries.GetCountries(), "Id", "Name");
            ViewData["CountryId"] = countryList;

            return View(data);
        }
        ////CREATE
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Movies moviess)
        {

            //if (moviess.MoviePhoto != null)
            //{
            //    string folder = "Movies/Cover/";
            //    folder += Guid.NewGuid().ToString() + "_" + moviess.MoviePhoto.FileName;
            //    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //    await moviess.MoviePhoto.CopyToAsync(new FileStream(serverfolder, FileMode.Create));

            //    _IMovies.AddMovies(moviess);
            //    TempData["MovieAdd"] = "Movie Added Successfully.";
            //    return RedirectToAction("Index");
            //}

            var image = Request.Form.Files.FirstOrDefault();
            var fileName = Guid.NewGuid().ToString();
            var path = $@"Movies\";
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploads = Path.Combine(wwwRootPath, path);
            var extension = Path.GetExtension(image.FileName);
            var x = Path.Combine(uploads, fileName + extension);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }
            moviess.MoviePhoto = $"\\Movies\\{fileName}" + extension;
            var isSuccess = _IMovies.AddMovies(moviess);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Movie created successfully";
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //DELETE
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Movies moviess = _IMovies.GetByID(id);
            //Drop down list for years
            var yearList = new SelectList(_Iyear.GetYearList(), "Id", "Years");
            ViewData["YearId"] = yearList;

            //Drop down list for Genre
            var genreList = new SelectList(_IGenre.GetAllGenre(), "Id", "Name");
            ViewData["GenreId"] = genreList;

            //Drop down list for Country
            var countryList = new SelectList(_ICountries.GetCountries(), "Id", "Name");
            ViewData["CountryId"] = countryList;

            return View(moviess);
        }

        [HttpPost]
        public IActionResult Delete(Movies moviess)
        {
            _IMovies.DeleteMovies(moviess);
            TempData["MovieRemove"] = "Movie Removed Successfully.";
            return RedirectToAction("Index");
        }

        //EDIT
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Movies moviess = _IMovies.GetByID(id);
            //Drop down list for years
            var yearList = new SelectList(_Iyear.GetYearList(), "Id", "Years");
            ViewData["YearId"] = yearList;

            //Drop down list for Genre
            var genreList = new SelectList(_IGenre.GetAllGenre(), "Id", "Name");
            ViewData["GenreId"] = genreList;

            //Drop down list for Country
            var countryList = new SelectList(_ICountries.GetCountries(), "Id", "Name");
            ViewData["CountryId"] = countryList;

            return View(moviess);
        }

        [HttpPost]
        public IActionResult Edit(Movies moviess)
        {

            var image = Request.Form.Files.FirstOrDefault();
            var fileName = Guid.NewGuid().ToString();
            var path = $@"Movies\";
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploads = Path.Combine(wwwRootPath, path);
            var extension = Path.GetExtension(image.FileName);
            var x = Path.Combine(uploads, fileName + extension);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }
            moviess.MoviePhoto = $"\\Movies\\{fileName}" + extension;

            _IMovies.UpdateMovies(moviess);
            TempData["MovieUpdate"] = "Movie Added Successfully.";
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult Details(int id)
        {
            Movies moviess = _IMovies.GetByID(id);
            //Drop down list for years
            var yearList = new SelectList(_Iyear.GetYearList(), "Id", "Years");
            ViewData["YearId"] = yearList;

            //Drop down list for Genre
            var genreList = new SelectList(_IGenre.GetAllGenre(), "Id", "Name");
            ViewData["GenreId"] = genreList;

            //Drop down list for Country
            var countryList = new SelectList(_ICountries.GetCountries(), "Id", "Name");
            ViewData["CountryId"] = countryList;

            ViewBag.Comments = _IComment.GetComments(id);

             
            return View(moviess);
        }


        [HttpPost]
        public IActionResult Details(Movies moviess) 
        {   
            return View(moviess);
        }
       
    }
}

