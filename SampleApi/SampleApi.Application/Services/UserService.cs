using SampleApi.Application.DTOs.Users;
using SampleApi.Application.Interfaces;
using SampleApi.Application.Results;
using SampleApi.Domain.Entities;

namespace SampleApi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<int>> CreateAsync(CreateUserDto dto)
    {
        try
        {
            var user = new User(
                dto.Username,
                createdById: null
            );

            var userId = await _userRepository.CreateAsync(user);

            return ServiceResult<int>.Ok(userId);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<int>.Fail(ex.Message);
        }
    }
}