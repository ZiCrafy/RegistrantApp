namespace RegistrantApp.Shared.Dto.Contragents;

public class dtoContragentUpdate
{
    public long ContragentID { get; set; }
    private string title;

    public string Title
    {
        get => title.ToUpper();
        set => title = value.ToUpper();
    }
}