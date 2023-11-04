namespace RegistrantApp.Shared.Dto.Documents;

public class dtoDocumentCreate
{
    public string Title { get; set; }
    public string? Seria { get; set; }
    public string? Number { get; set; }
    public string? Authority { get; set; }
    public DateOnly DateOfIssue { get; set; }
}