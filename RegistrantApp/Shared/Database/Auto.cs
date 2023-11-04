using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class Auto
{
    [Key] public long AutoID { get; set; }
    public string Title { get; set; }
    public string AutoNumber { get; set; }
    public Account OwnerAuto { get; set; }
    public bool IsDeleted { get; set; }
}