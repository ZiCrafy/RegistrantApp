namespace RegistrantApp.Shared.Dto.Auto;

public class dtoAutoUpdate
{
    public long AutoID { get; set; }
    private string title;
    public string Title
    {
        get => title.ToUpper();
        set => title = value.ToUpper();
    }

    private string autoNumber;
    public string AutoNumber
    {
        get => autoNumber.ToUpper();
        set => autoNumber = value.ToUpper();
    }
    public long OwnerAutoId { get; set; }
}