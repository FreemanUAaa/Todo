namespace Todo.Users.Core.Options
{
    public class EmailOptions
    {
        public string Host { get; set; }   

        public int Port { get; set; }

        public string From { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public EmailOptions(string host, int port, string from, string email, string password) => 
            (Host, Port, From, Email, Password) = (host, port, from, email, password);
    }
}
