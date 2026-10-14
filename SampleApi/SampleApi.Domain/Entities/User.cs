namespace SampleApi.Domain.Entities
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public int? CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
