using SampleApi.Domain.Common;

namespace SampleApi.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public string Username { get; private set; } = string.Empty;
        private User() { }

        public User(string username, int? createdById)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");

            Username = username;
            CreatedById = createdById;
            DateCreated = DateTime.UtcNow;
        }

        public void UpdateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");

            Username = username;
            DateUpdated = DateTime.UtcNow;
        }
    }
}
