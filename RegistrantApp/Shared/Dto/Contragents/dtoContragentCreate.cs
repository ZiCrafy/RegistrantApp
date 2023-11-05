namespace RegistrantApp.Shared.Dto.Contragents;

public class dtoContragentCreate
{
    private string title;

    public string Title
    {
        get => title.ToUpper();
        set => title = value.ToUpper();
    }

}