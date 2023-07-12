using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GenreController : Controller
    {
        private readonly IGenre _Igenre;
        public GenreController(IGenre genres) //Creating Object for Interface
        {
            _Igenre= genres;    
        }

        public IActionResult Index()
        {
            var data = _Igenre.GetAllGenre();
            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var data = new Genre();
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Genre genress)
        {
            _Igenre.AddGenre(genress);
            TempData["GenreAdd"] = "Genre Added Successfully.";
            return RedirectToAction("Index");
        }

        //DELETE
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Genre genress)
        {
            _Igenre.DeleteGenre(genress);
            TempData["GenreRemove"] = "Genre Removed Successfully.";
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Genre genre = _Igenre.GetByID(id);
            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genress)
        {
            _Igenre.UpdateGenre(genress);
            TempData["GenreUpdate"] = "Genre Updated Successfully.";
            return RedirectToAction("Index");
        }
    }
}
