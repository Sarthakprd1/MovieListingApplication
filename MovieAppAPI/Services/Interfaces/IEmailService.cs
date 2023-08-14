using MovieAppAPI.DTO;

namespace MovieAppAPI.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
