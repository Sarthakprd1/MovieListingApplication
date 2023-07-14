using Microsoft.AspNetCore.Mvc;

namespace MovieListing.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
