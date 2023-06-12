using Microsoft.EntityFrameworkCore;
using Shared.DatabaseInitializer;

namespace UserService.DAL;

public class UserDataBaseInitializer : DataBaseInitializer
{
    public UserDataBaseInitializer(UserContext.UserContext context) : base(context)
    {
    }
}