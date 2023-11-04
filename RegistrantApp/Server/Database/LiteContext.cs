using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Database.Base;

namespace RegistrantApp.Server.Database;

public class LiteContext : RaContext
{
    private readonly IConfiguration _config;

    public LiteContext(IConfiguration config)
    {
        _config = config;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_config["connectionString"]);
    }
    
}