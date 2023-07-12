using Microsoft.AspNetCore.Mvc;
using MovieListing.Repository.Interfaces;

namespace MovieListing.Controllers
{
    public class CommentController : Controller
    {
        private readonly IComment _icomment;
        public CommentController(IComment icomment)
        {
            _icomment = icomment;
        }
        public IActionResult Index()
        {
            var result = _icomment.GetComments();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create() 
        {
            return View();
        }
    }
}
