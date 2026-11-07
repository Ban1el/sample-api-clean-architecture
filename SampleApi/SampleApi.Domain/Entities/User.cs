using SampleApi.Domain.Common;

namespace SampleApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        private User() { }

        public User(string username, int? createdById)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");

            Username = username;
            CreatedById = createdById;
            DateCreated = DateTime.UtcNow;
        }
    }
}
