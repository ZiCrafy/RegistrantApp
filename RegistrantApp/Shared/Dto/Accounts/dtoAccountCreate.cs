using RegistrantApp.Shared.Validators;

namespace RegistrantApp.Shared.Dto.Accounts;

public class dtoAccountCreate
{
    private string firstName;

    public string FirstName
    {
        get => firstName.ToUpper();
        set => firstName = value.ToUpper();
    }

    private string middleName;

    public string MiddleName
    {
        get => middleName.ToUpper();
        set => middleName = value.ToUpper();
    }

    private string? lastName;

    public string? LastName
    {
        get => lastName?.ToUpper();
        set => lastName = value?.ToUpper();
    }

    private long phoneNumber;

    public long PhoneNumber
    {
        get => phoneNumber;
        set => phoneNumber = MyValidator.PhoneMinimalValidation(value!.ToString());
    }

    private string? password;

    public string? Password
    {
        get => password;
        set => password = MyValidator.CreateMD5(value!);
    }

    public bool IsEmployee { get; set; }
}