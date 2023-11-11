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
}