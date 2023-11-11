using RegistrantApp.Shared.Validators;

namespace RegistrantApp.Shared.Dto.Security;

public class dtoCredentials
{
    public string Login { get; set; }
    public string? Password { get; set; }
    public string? IpAdress { get; set; }
    public string? FingerPrint { get; set; }
}