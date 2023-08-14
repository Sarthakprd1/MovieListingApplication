using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MovieAppAPI.DTO;
using MovieAppAPI.Services.Interfaces;
using MovieListing.Repository.Interfaces;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IMovies _IMovies;

        public EmailController(IEmailService emailService, IMovies movies)
        {
            _emailService = emailService;
            _IMovies = movies;
        } 

        [HttpPost]
        public IActionResult SendEmail(EmailDTO request)
        {
            _emailService.SendEmail(request);
            return Ok();
        }

        [HttpPost("{id}"), Authorize(Roles = "Admin")]
        public IActionResult SendMovieDetails(int id, EmailDTO request)
        {
            var fetchmovie = _IMovies.GetByID(id);
            
            var x = new EmailDTO
            {
                To= request.To,
                Body= $"Here Includes the Details of\n Movie Title: {fetchmovie.Title} Country: {fetchmovie.CountryRefId} Year:  {fetchmovie.YearId}", 
                Subject= request.Subject,
            };
            _emailService.SendEmail(x);
            return Ok();
        }
    }
}
