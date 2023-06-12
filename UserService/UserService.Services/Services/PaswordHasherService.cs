using System.Security.Cryptography;
using System.Text;
using UserService.Services.Services.Abstract;

namespace UserService.Services.Services;

public class PaswordHasherService : IPaswordHasherService
{
    public Task<string> HashPasswordAsync(string password)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

        return Task.FromResult(Convert.ToBase64String(hash));
    }
}
