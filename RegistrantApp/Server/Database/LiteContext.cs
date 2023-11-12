using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using Audit = RegistrantApp.ClientApi.Controllers.Audit;

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