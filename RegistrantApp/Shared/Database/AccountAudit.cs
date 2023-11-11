using Microsoft.EntityFrameworkCore;

namespace RegistrantApp.Shared.Database;

[Keyless]
public class AccountAudit : Account
{
    public DateTime DateTimeImplementChanges { get; set; }
}