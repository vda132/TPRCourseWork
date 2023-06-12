using Microsoft.EntityFrameworkCore;
using UserService.DAL.Models;
using UserService.Dtos;
using Shared.Responce;

namespace UserService.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext.UserContext context;
    public UserRepository(UserContext.UserContext context) =>
        this.context = context;
    
    public async Task<ResponceModel<Guid>> AddAsync(UserDto model, CancellationToken cancellationToken)
    {
        try
        {
            var id = Guid.NewGuid();
            model.Id = id;

            await this.context.AddAsync(model.ToEntity(), cancellationToken);
            await this.context.SaveChangesAsync();

            return new ResponceModel<Guid>
            {
                Data = id,
                ExceptionError = null,
                HasErrors = false
            };
        }
        catch (Exception ex)
        {
            return new ResponceModel<Guid>
            {
                Data = default,
                ExceptionError = ex.Message,
                HasErrors = true
            };
        }
    }

    public async Task<ResponceModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await this.context.Users.FindAsync(new object[] { id }, cancellationToken);

        if (user is null)
            return new ResponceModel
            {
                ExceptionError = "User not found",
                HasErrors = true
            };

        this.context.Users.Remove(user);
        await this.context.SaveChangesAsync();

        return new ResponceModel
        {
            ExceptionError = null,
            HasErrors = false
        };
    }

    public async Task<ResponceModel<UserDto>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await this.context.Users.FindAsync(new object[] { id }, cancellationToken);

        return new ResponceModel<UserDto>
        {
            Data = user?.ToDto(),
            ExceptionError = user is null ? "User not found" : null,
            HasErrors = user is null
        };
    }

    public async Task<ResponceModel<IEnumerable<UserDto>>> GetAsync(CancellationToken cancellationToken)
    {
        var users = await this.context.Users.ToListAsync();

        return new ResponceModel<IEnumerable<UserDto>>
        {
            Data = users.Select(el => el.ToDto()),
            ExceptionError = null,
            HasErrors = false
        };
    }

    public async Task<ResponceModel<UserDto>> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        var user = await this.context.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);
        
        return new ResponceModel<UserDto> { Data = user?.ToDto(), ExceptionError = null, HasErrors = false };
    }

    public async Task<ResponceModel<UserDto>> UpdateAsync(UserDto model, CancellationToken cancellationToken)
    {
        var user = await this.context.Users.FindAsync(new object[] { model.Id }, cancellationToken);

        if (user is null)
            return new ResponceModel<UserDto>
            {
                Data = null,
                ExceptionError = "User not found",
                HasErrors = true
            };

        user.Login = model.Login;
        
        if(model.Password is not null) 
            user.Password = model.Password;
        
        user.Name = model.Name;
        user.Role = model.Role;

        await context.SaveChangesAsync();

        return new ResponceModel<UserDto>
        { 
            Data = user.ToDto(),
            ExceptionError = null,
            HasErrors = false
        };
    }
}
