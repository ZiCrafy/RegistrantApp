using Microsoft.EntityFrameworkCore;
using RegistrantApp.Shared.Database;
using File = RegistrantApp.Shared.Database.File;

namespace RegistrantApp.Server.Database.Base;

public class RaContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Auto> Autos { get; set; }
    public DbSet<Contragent> Contragents { get; set; }
    public DbSet<Audit> Audit { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Document> Documents { get; set; }

    public async Task AuditChanges(Account account)
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

                var audit = new Audit()
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