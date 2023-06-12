using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos;

public class RegisterDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Login { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    public required string Role { get; set; }
}

public static class Extencion 
{
    public static UserDto ToUserDto(this RegisterDto dto) =>
        new UserDto
        {
            Id = Guid.Empty,
            Name = dto.Name,
            Login = dto.Login,
            Password = dto.Password,
            Role = dto.Role
        };
    
}
