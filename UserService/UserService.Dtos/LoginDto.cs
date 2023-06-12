using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "Login is required")]
    public required string Login { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}
