using Shared.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UserService.Dtos;

public class UserDto : BaseEntity<Guid>
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Login { get; set; }
    [AllowNull]
    public string? Password { get; set; }
    [Required]
    public required string Role { get; set; }
}
