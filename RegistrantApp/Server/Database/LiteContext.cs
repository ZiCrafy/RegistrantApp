using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;

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

    /*public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await AuditChanges();
        return base.SaveChangesAsync(cancellationToken);
    }


    public async Task AuditChanges()
    {
        var entityEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added ||
                        x.State == EntityState.Modified ||
                        x.State == EntityState.Deleted).ToList();


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

                if (originalValue == currentValue) continue;
                
                var changeAudit = await GetChangeAuditAsync(entityEntry, timeStamp);
                
                changeAudit.PropertyName = prop.Name;
                changeAudit.OldValue = originalValue;
                changeAudit.NewValue = currentValue;
                
                await this.AddAsync(changeAudit);
            }
        }
    }*/
}