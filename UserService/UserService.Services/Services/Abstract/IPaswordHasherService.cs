namespace UserService.Services.Services.Abstract;

public interface IPaswordHasherService
{
    Task<string> HashPasswordAsync(string password);
}
