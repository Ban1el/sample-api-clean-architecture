using SampleApi.Application.DTOs.Users;
using SampleApi.Application.Interfaces.Repositories;
using SampleApi.Application.Interfaces.Services;
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

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var deleted = await _userRepository.DeleteAsync(id);

        if (!deleted)
            return ServiceResult<bool>.Fail("User not found.");

        return ServiceResult<bool>.Ok(true);
    }

    public async Task<ServiceResult<UserDto>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return ServiceResult<UserDto>.Fail("User not found.");

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            DateCreated = user.DateCreated,
            DateUpdated = user.DateUpdated
        };

        return ServiceResult<UserDto>.Ok(userDto);
    }

    public async Task<ServiceResult<bool>> UpdateAsync(
    int id,
    UpdateUserDto dto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return ServiceResult<bool>.Fail("User not found.");

            user.UpdateUsername(dto.Username);

            var updated = await _userRepository.UpdateAsync(user);

            return ServiceResult<bool>.Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<bool>.Fail(ex.Message);
        }
    }
}