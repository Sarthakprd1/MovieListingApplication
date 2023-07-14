using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using MovieListing.Repository.Interfaces;
using System.Security.Claims;

namespace MovieListing.Controllers
{
    public class CommentController : Controller
    {
        private readonly IComment _iComment;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(IComment icomment, UserManager<IdentityUser> userManager)
        {
            _iComment = icomment;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    var result = _icomment.GetComments();
        //    return View(result);
        //}
       
        [HttpPost]
        public IActionResult Create([Bind("MovieId,CommentDesc")]Comment comment) 
        {
            comment.UserId = _userManager.GetUserId(User);
            comment.TimeStamp= DateTime.Now;
            _iComment.AddComments(comment);
            TempData["Comment Addition"] = "Comment Added Successfully.";
            return RedirectToAction("Details", "Movies", new {id=comment.MovieId}); 
        }

        //DELETE
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var comment = _iComment.GetById(id);
            _iComment.DeleteComments(comment);
            TempData["DeleteComment"] = "Comment Removed Successfully.";
            return RedirectToAction("Details", "Movies", new { id = comment.MovieId });
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {

            Comment comment = _iComment.GetById(id);
            return View(comment); 
        }

        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            comment.UserId = claim.Value;
            comment.TimeStamp= DateTime.Now;    
            _iComment.EditComments(comment);
            TempData["UpdateComment"] = "Comment Updated Successfully.";
            return RedirectToAction("Details", "Movies", new { id = comment.MovieId });
        }
    }
}
