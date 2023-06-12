using UserService.DAL.Models;
using UserService.Dtos;
using Shared.BaseActions;
using Shared.Responce;

namespace UserService.DAL.Repositories;

public interface IUserRepository : ICRUDActions<Guid, UserDto>
{
    Task<ResponceModel<UserDto>> GetUserByLogin(string login, CancellationToken cancellationToken);
}
