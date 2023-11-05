using RegistrantApp.Shared.Validators;

namespace RegistrantApp.Shared.Dto.Security;

public class dtoCredentials
{
    public string Login { get; set; }
    private string? password;

    public string? Password
    {
        get => password;
        set => password = MyValidator.CreateMD5(value?.ToString());
    }
    public string? IpAdres { get; set; }
    public string? FingerPrint { get; set; }
}