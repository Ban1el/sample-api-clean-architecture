namespace SampleApi.Application.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
    }
    public class UpdateUserDto
    {
        public string Username { get; set; } = string.Empty;
    }
}
