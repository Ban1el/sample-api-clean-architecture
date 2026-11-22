using SampleApi.Application.DTOs.Users;
using SampleApi.Application.Results;

namespace SampleApi.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ServiceResult<int>> CreateAsync(CreateUserDto dto);
        Task<ServiceResult<bool>> UpdateAsync(int id, UpdateUserDto dto);
        Task<ServiceResult<UserDto>> GetByIdAsync(int id);
    }
}
