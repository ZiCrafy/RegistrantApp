using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class Token
{
    [Key] public string TokenID { get; set; }
    public DateTime DateTimeSessionStarted { get; set; }
    public DateTime DateTimeSessionExpired { get; set; }
    public string? IPv4 { get; set; }
    public string? FingerPrint { get; set; }
    public Account OwnerToken { get; set; }
}