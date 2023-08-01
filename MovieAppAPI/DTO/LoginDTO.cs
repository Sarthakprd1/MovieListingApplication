namespace MovieAppAPI.DTO
{
    public class LoginDTO
    {
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
    }
}
