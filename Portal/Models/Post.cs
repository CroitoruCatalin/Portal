using Portal.Models.DTO;
using System.Text.Json.Serialization;

namespace Portal.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string? AuthorID { get; set; }

        [JsonIgnore]
        public User Author { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }

        public void SetFromDTO(PostDTO dto)
        {
            //PostID = dto.PostID;
            AuthorID = dto.AuthorID;
            Message = dto.Message;
            CreationDate = dto.CreationDate;
        }
    }
}
