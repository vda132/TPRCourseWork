using Microsoft.EntityFrameworkCore;

namespace Shared.DatabaseInitializer;

public interface IDatabaseInitializer
{
    Task SeedAsync();
}

public abstract class DataBaseInitializer : IDatabaseInitializer
{
    private DbContext context;

    protected DataBaseInitializer(DbContext context) 
    {
        this.context = context;
    }
    

    public async Task SeedAsync()
    {
        await this.context.Database.MigrateAsync().ConfigureAwait(false);
    }
}
