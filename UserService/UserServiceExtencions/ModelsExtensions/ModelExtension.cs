using UserService.DAL.Models;
using UserService.Dtos;
namespace UserServiceExtensions;

public static class ModelExtension
{
    public static User ToEntity(this UserDto dto) =>
        new User
        {
            Id = dto.Id,
            Name = dto.Name,
            Login = dto.Login,
            Password = dto.Password,
            Role = dto.Role
        };

    public static UserDto ToDto(this User model) =>
        new UserDto
        {
            Id = model.Id,
            Name = model.Name,
            Login = model.Login,
            Password = model.Password,
            Role = model.Role
        };
}
