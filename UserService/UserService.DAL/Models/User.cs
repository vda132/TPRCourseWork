using UserService.Dtos;
using Shared.BaseEntity;

namespace UserService.DAL.Models;

public class User : BaseEntity<Guid>
{
    public required string Name { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }

}

public static class ModelExtension
{
    public static User ToEntity(this UserDto dto) =>
        new User
        {
            Id = dto.Id,
            Name = dto.Name,
            Login = dto.Login,
            Password = dto.Password!,
            Role = dto.Role
        };

    public static UserDto ToDto(this User model) =>
        new UserDto
        {
            Id = model.Id,
            Name = model.Name,
            Login = model.Login,
            Role = model.Role
        };
}

