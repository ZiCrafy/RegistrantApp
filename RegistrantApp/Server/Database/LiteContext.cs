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
    
    public override async Task AuditChanges(Account account)
    {
        var entityEntries = ChangeTracker.Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted).ToList();

        var ownerEvent = $"{account.FirstName} {account.MiddleName.First()}. {account.LastName?.First()}.";
        
        foreach (var entityEntry in entityEntries)
        {
            foreach (var prop in entityEntry.OriginalValues.Properties)
            {
                var originalValue = !string.IsNullOrWhiteSpace(entityEntry.OriginalValues[prop]?.ToString())
                    ? entityEntry.OriginalValues[prop]?.ToString()
                    : null;

                var currentValue = !string.IsNullOrWhiteSpace(entityEntry.CurrentValues[prop]?.ToString())
                    ? entityEntry.CurrentValues[prop]?.ToString()
                    : null;

                if ((originalValue == currentValue) && (entityEntry.State != EntityState.Added)) continue;

                var audit = new RegistrantApp.Shared.Database.Audit()
                {
                    DateTimeEvent = DateTime.Now,
                    OwnerEvent = ownerEvent,
                    Object = entityEntry.Entity.GetType().ToString()!,
                    Action = entityEntry.State.ToString(),
                    Property = prop.Name,
                    ValueAfter = originalValue,
                    ValueBefore = currentValue
                };
                
                await AddAsync(audit);
            }
        }
        
        await SaveChangesAsync();
    }
}