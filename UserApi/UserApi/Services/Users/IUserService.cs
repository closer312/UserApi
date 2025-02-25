using UserApi.Dtos.Users;

namespace UserApi.Services.Users;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserAsync(int id);
    Task UpdateUserFieldsAsync(List<UserFormDto> userFormDtos);
    Task DeleteUserAsync(int id);
}
