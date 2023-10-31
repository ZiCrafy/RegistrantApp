using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class Account 
{
    [Key] public long AccountID { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string? LastName { get; set; }
    public long PhoneNumber { get; set; }
    public string? Password { get; set; }
    public bool IsEmployee { get; set; }
    public bool IsSpecialLoginUI { get; set; }
    public bool IsDeleted { get; set; }
}