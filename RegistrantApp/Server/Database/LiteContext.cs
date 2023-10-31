using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Database.Models;

namespace RegistrantApp.Server.Database;

public class LiteContext : DbSetTables
{
    private readonly IConfiguration _config;

    public LiteContext(IConfiguration config)
    {
        _config = config;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = LocalStorage.db");
    }
    
}