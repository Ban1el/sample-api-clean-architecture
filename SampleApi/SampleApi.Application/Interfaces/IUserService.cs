using SampleApi.Application.DTOs.Users;
using SampleApi.Application.Results;

namespace SampleApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<int>> CreateAsync(CreateUserDto dto);
    }
}
