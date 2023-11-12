using RegistrantApp.Shared.Validators;

namespace RegistrantApp.Shared.Dto.Security;

public class dtoChangeCredentialPassword
{
    public string OldPassword { get; set; }

    private string newPassword;

    public string NewPassword
    {
        get => newPassword;
        set => newPassword = MyValidator.CreateMd5(value?.ToString());
    }
}