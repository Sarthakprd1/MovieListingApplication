using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Services;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        Mailobj _oMailObj = new Mailobj();
        [HttpPost]
        [Route("Welcome")]
        public IActionResult Welcome(string username)
        {
            var jobId = BackgroundJob.Enqueue(() => _oMailObj.SendWelcomeMail(username));
            return Ok($"JobId :  {jobId} Completed. Welcome Mail Sent.");
        }

        [HttpPost]
        [Route("Delayed")]
        public IActionResult Delayed(string username)
        {
            var jobId = BackgroundJob.Schedule(() => _oMailObj.SendDelayedWelcomeMail(username), TimeSpan.FromMinutes(1));
            return Ok($"JobId :  {jobId} Scheduled (Mail will be sent after 1 minute.)");
        }

        [HttpPost]
        [Route("Invoice")]
        public IActionResult Invoice(string username)
        {
            RecurringJob.AddOrUpdate(() => _oMailObj.SendInvoiceMail(username), Cron.Monthly);
            return Ok($"RecurringJob Scheduled (Monthly) for {username} ");
        }

        [HttpPost]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe(string username)
        {
            var jobId = BackgroundJob.Enqueue(() => _oMailObj.UnsubscribeUser(username));
            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine($"JobId : {jobId}, confirmation Mail Send to {username}"));
            return Ok($"Unsubscribed");
        }

    }
}
