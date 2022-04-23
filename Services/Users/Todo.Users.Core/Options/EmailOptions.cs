namespace Todo.Users.Core.Options
{
    public class EmailOptions
    {
        public string Host { get; set; }   

        public string Post { get; set; }

        public EmailOptions(string host, string post) => 
            (Host, Post) = (host, post);
    }
}
