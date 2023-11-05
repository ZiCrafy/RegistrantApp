using RegistrantApp.Shared.Validators;

namespace RegistrantApp.Shared.Dto.Security;

public class dtoChangeCredentialPassword
{
    public string OldPassword { get; set; }

    private string newPassword { get; set; }
    public string NewPassword
    {
        get => newPassword;
        set => MyValidator.CreateMD5(value?.ToString());
    }
}