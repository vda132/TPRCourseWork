using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Services.Services;

using Roles = Shared.Constants.Roles;

namespace UserService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        return Ok(await this.userService.GetAsync(cancellationToken));
    }

    [HttpGet("auth")]
    [Authorize]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        var id = User.FindFirst(el => el.Type == "id");
        var result = await this.userService.GetAsync(Guid.Parse(id!.Value), cancellationToken);
        return result.HasErrors ? BadRequest(result) : Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await this.userService.GetAsync(id, cancellationToken);
        return result.HasErrors ? BadRequest(result) : Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] RegisterDto user, CancellationToken cancellationToken)
    {
        var result = await this.userService.AddAsync(user.ToUserDto(), cancellationToken);
        return result.HasErrors ? BadRequest(result) : Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto login, CancellationToken cancellationToken)
    {
        var result = await this.userService.Login(login, cancellationToken);
        return result.HasErrors ? BadRequest(result) : Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await this.userService.DeleteAsync(id, cancellationToken);
        return result.HasErrors ? BadRequest(result) : Ok(result);
    }
}
