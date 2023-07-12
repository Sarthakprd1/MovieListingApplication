using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly ICountries _iCountries;
        public CountriesController(ICountries countriess)
        {
            _iCountries = countriess;
        }

        //INDEX Page
        public IActionResult Index()
        {
            var result = _iCountries.GetCountries();
            return View(result);
        }

        //Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var data = new Country();
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Country countries)
        {
            _iCountries.AddCountries(countries);
            TempData["AlertMessage"] = "Country Added Successfully.";
            return RedirectToAction("Index");
            
        }
        /*[HttpGet]
        public IActionResult Delete(int id)
        {
            country country = _iCountries.GetByID(id);
            return View(country);
        }*/
        //Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var country = _iCountries.GetByID(id);
            _iCountries.DeleteCountries(country);
            TempData["DeleteMessage"] = "Country Removed Successfully.";
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Country country = _iCountries.GetByID(id);
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Country country)
        {
            _iCountries.EditCountry(country);
            TempData["UpdateCountry"] = "Country Updated Successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int id)
        {

            return View();
        }

        [HttpPost]
        public IActionResult Detail()
        {
            return View();
        }
    }
}

