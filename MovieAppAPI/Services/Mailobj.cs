namespace MovieAppAPI.Services
{
    public class Mailobj
    {
        public void SendWelcomeMail(string username) 
        {
            Console.WriteLine($"SendWelcomeMail : {username}");
        }

        public void SendDelayedWelcomeMail(string username) 
        {
            Console.WriteLine($"SendDelayedWelcomeMail : {username}");
        }
        public void SendInvoiceMail(string username) 
        {
            Console.WriteLine($"SendInvoiceMail : {username}");
        }
        public void UnsubscribeUser(string username) 
        {
            Console.WriteLine($"UnsubscribeUser : {username}");
        }
    }
}
