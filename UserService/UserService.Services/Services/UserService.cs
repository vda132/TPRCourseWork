using UserService.DAL.Repositories;
using UserService.Dtos;
using UserService.Services.Services.Abstract;
using Shared.Responce;

namespace UserService.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IJwtService jwtService;
    private readonly IPaswordHasherService hasherService;

    public UserService(IUserRepository userRepository, IJwtService jwtService, IPaswordHasherService hasherService)
    {
        this.userRepository = userRepository;
        this.jwtService = jwtService;
        this.hasherService = hasherService;
    }

    public async Task<ResponceModel<Guid>> AddAsync(UserDto model, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.GetUserByLogin(model.Login, cancellationToken);

        if (user.Data != null)
            return new ResponceModel<Guid>
            {
                Data = default,
                ExceptionError = "User with this login is exist",
                HasErrors = false
            };

        model.Password = await this.hasherService.HashPasswordAsync(model.Password!);
        return await this.userRepository.AddAsync(model, cancellationToken);
    }

    public async Task<ResponceModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.userRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<ResponceModel<UserDto>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
         return await this.userRepository.GetAsync(id, cancellationToken);
    }

    public async Task<ResponceModel<IEnumerable<UserDto>>> GetAsync(CancellationToken cancellationToken)
    {
        return await this.userRepository.GetAsync(cancellationToken);
    }

    public async Task<ResponceModel<string>> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.GetUserByLogin(dto.Login, cancellationToken);

        if (user.Data is null) 
            return new ResponceModel<string>
            {
                Data = null,
                ExceptionError = "Invalid login or password",
                HasErrors = true
            };

        if (user.Data!.Password == (await this.hasherService.HashPasswordAsync(dto.Login)))
            return new ResponceModel<string>
            {
                Data = null,
                ExceptionError = "Invalid login or password",
                HasErrors = true
            };

        return this.jwtService.CreateToken(user?.Data!);
    }

    public async Task<ResponceModel<UserDto>> UpdateAsync(UserDto model, CancellationToken cancellationToken)
    {
        model.Password = model.Password != null ? (await this.hasherService.HashPasswordAsync(model.Password)) : null!;

        return await this.userRepository.UpdateAsync(model, cancellationToken);
    }
}
