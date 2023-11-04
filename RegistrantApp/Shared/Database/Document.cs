using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class Document
{
    [Key] public long DocumentID { get; set; }
    public string Title { get; set; }
    public string? Seria { get; set; }
    public string? Number { get; set; }
    public string? Authority { get; set; }
    public DateOnly DateOfIssue { get; set; }
    public bool IsDeleted { get; set; }
}