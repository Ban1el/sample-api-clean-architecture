using SampleApi.Application.DTOs.Users;
using SampleApi.Application.Results;
using SampleApi.Domain.Entities.Identity;

namespace SampleApi.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
