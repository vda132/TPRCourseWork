
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Dtos;
using Shared.Responce;

namespace UserService.Services;

public interface IJwtService 
{
    ResponceModel<string> CreateToken(UserDto dto);
}
public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;

    public JwtService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public ResponceModel<string> CreateToken(UserDto dto)
    {
        var claims = new List<Claim>()
        {
            new Claim("id", dto.Id.ToString()),
            new Claim(ClaimTypes.Role, dto.Role)
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Key"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
                  signingCredentials: signingCredentials,
                  claims: claims,
                  expires: DateTime.Now.AddHours(1));

        return new ResponceModel<string>
        {
            Data = new JwtSecurityTokenHandler().WriteToken(jwt),
            ExceptionError = null,
            HasErrors = false
        };
    }
}
