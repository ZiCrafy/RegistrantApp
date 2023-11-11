using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class Audit 
{
    [Key] public long EventID { get; set; }
    public DateTime DateTimeEvent { get; set; }
    public string OwnerEvent { get; set; }
    public string Object { get; set; }
    public string? Action { get; set; }
    public string? Property { get; set; }
    public string? ValueAfter { get; set; }
    public string? ValueBefore { get; set; }
}