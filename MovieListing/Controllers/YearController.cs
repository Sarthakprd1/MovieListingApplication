using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;
using System.Data;

namespace MovieListing.Controllers
{
    [Authorize(Roles ="Admin")]
    public class YearController : Controller
    {
        private readonly IYear _Iyear; //Calling Interface
        public YearController(IYear year) //Creating Object for IYear
        {
            _Iyear = year;  
        } 
        
        public IActionResult Index(int pg = 1)
        {
            var data = _Iyear.GetYearList();            
            const int pageSize = 5;
            if (pg < 1)

                pg = 1;
            int recsCount = data.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var datas = data.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
            
        }

        //CREATE
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var data = new Year();
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Year yearss)
        {
            _Iyear.AddDb(yearss);   
            TempData["YearAdd"] = "Year Added Successfully.";
            return RedirectToAction("Index");
        }

        //DELETE
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Year yearss)
        {
            _Iyear.DeleteDb(yearss);
            TempData["YearRemove"] = "Year Removed Successfully.";
            return RedirectToAction("Index");
            
        }

        //EDIT
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Year yearss = _Iyear.GetById(id); 
            return View(yearss);
            
        }

        [HttpPost]
        public IActionResult Edit(Year yearss)
        {
            _Iyear.UpdateDb(yearss);    
            TempData["YearUpdate"] = "Year Updated Successfully.";
            return RedirectToAction("Index");
            
        }

    }
}
