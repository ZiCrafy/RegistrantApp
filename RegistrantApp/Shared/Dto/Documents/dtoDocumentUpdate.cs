namespace RegistrantApp.Shared.Dto.Documents;

public class dtoDocumentUpdate
{
    public long DocumentID { get; set; }
    private string title;

    public string Title
    {
        get => title.ToUpper();
        set => title = value.ToUpper();
    }

    private string? seria;

    public string? Seria
    {
        get => seria?.ToUpper();
        set => seria = value?.ToUpper();
    }

    private string? number;

    public string? Number
    {
        get => number?.ToUpper();
        set => number = value?.ToUpper();
    }

    private string? authority;

    public string? Authority
    {
        get => authority?.ToUpper();
        set => authority = value?.ToUpper();
    }
    public DateOnly DateOfIssue { get; set; }
}