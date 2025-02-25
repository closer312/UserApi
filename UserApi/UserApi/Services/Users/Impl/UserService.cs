using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Dtos.Users;
using UserApi.Enums;
using UserApi.Models.Users;

namespace UserApi.Services.Users.Impl;

public class UserService : IUserService
{
    private readonly AppPostgreSqlDbContext _context;
    private readonly IMapper _mapper;
    public UserService(AppPostgreSqlDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserAsync(int id)
    {
        var user = await _context.Users
            .Include(x => x.UserFields).ThenInclude(x => x.Field)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new Exception("Пользователь не найден в базе данных.");

        var result = _mapper.Map<UserDto>(user);
        return result;
    }
    public async Task UpdateUserFieldsAsync(List<UserFormDto> userFormDtos)
    {
        foreach (var item in userFormDtos)
        {
            var userField = await _context.UserFields
            .Include(x => x.Field)
            .FirstOrDefaultAsync(x => x.Id == item.UserFieldId);

            if (userField == null)
                throw new Exception("Поле не найдено в базе данных.");

            await UpdateField(userField, item);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var users = await _context.Users
            .Include(x => x.UserFields).ThenInclude(x => x.Field).ToListAsync();
        var result = _mapper.Map<List<UserDto>>(users);
        return result;
    }

    private async Task UpdateField(UserField userField, UserFormDto dto)
    {
        if (Enum.TryParse<FieldType>(userField.Field.FieldType, out var type))
        {
            switch (type)
            {
                case FieldType.Id:
                    if (long.TryParse(dto.FieldValue, out long value))
                    {
                        userField.FieldValue = value.ToString();
                    }
                    break;
                case FieldType.Text:
                    userField.FieldValue = dto.FieldValue;
                    break;
            }
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            throw new Exception("Пользователь не найден в базе данных.");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
