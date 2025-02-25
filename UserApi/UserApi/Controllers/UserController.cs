using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos.Users;
using UserApi.Services.Users;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("get-users")]
    public async Task<List<UserDto>> GetUsers()
    {
        var result = await _userService.GetUsersAsync();
        return result;
    }
    [HttpGet("get-user/{id}")]
    public async Task<UserDto> GetUser(int id)
    {
        var result = await _userService.GetUserAsync(id);
        return result;
    }
    [HttpPut("update-user-fields")]
    public async Task<IActionResult> UpdateUserFields(List<UserFormDto> userFormDtos)
    {
        await _userService.UpdateUserFieldsAsync(userFormDtos);
        return Ok();
    }
    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
}
