using SampleApi.Domain.Entities;

namespace SampleApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user);
    }
}
