using System.Text.Json.Serialization;

namespace Portal.Models.DTO
{
    public class UpdatePostDTO
    {
        public int PostID { get; set; }
        [JsonIgnore]
        public string? AuthorID { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; }

        public void Set(Post post)
        {
            PostID = post.PostID;
            AuthorID = post.AuthorID;
            Message = post.Message;
            CreationDate = post.CreationDate;
        }
    }
}
