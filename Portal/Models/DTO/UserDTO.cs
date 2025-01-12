namespace Portal.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }

        public void SetFromUser(User user)
        {
            Username = user.UserName;
        }
    }
}
