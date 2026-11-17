using Microsoft.AspNetCore.Mvc;
using SampleApi.Application.Interfaces;
using SampleApi.Application.DTOs.Users;
using SampleApi.Contracts.Responses;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var result = await _userService.CreateAsync(dto);

        if (!result.Success)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "User Creation Failed",
                Detail = result.Error
            });
        }

        var response = new ApiResponse<int>
        {
            Success = true,
            Message = "User created successfully.",
            Data = result.Data
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data },
            response
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateUserDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);

        if (!result.Success)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "User Update Failed",
                Detail = result.Error
            });
        }

        var response = new ApiResponse<bool>
        {
            Success = true,
            Message = "User updated successfully.",
            Data = result.Data
        };

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _userService.GetByIdAsync(id);

        if (!result.Success)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User Not Found",
                Detail = result.Error
            });
        }

        var response = new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User retrieved successfully.",
            Data = result.Data
        };

        return Ok(response);
    }
}