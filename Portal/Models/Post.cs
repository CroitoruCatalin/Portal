namespace Portal.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string? AuthorID { get; set; }
        public User Author { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
