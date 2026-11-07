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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        throw new NotImplementedException();
    }
}