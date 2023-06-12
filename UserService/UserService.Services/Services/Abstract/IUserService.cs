using UserService.Dtos;
using Shared.BaseActions;
using Shared.Responce;

namespace UserService.Services.Services;

public interface IUserService : ICRUDActions<Guid, UserDto>
{
    Task<ResponceModel<string>> Login(LoginDto dto, CancellationToken cancellationToken);
}
