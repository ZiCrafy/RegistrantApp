using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class File 
{
    [Key] public Guid FileID { get; set; }
    public string FileName { get; set; }
    public byte[] Bytes { get; set; }
    public DateTime DateTimeUpload { get; set; }
    
    public Document? Document { get; set; }
    public Order? Order { get; set; }
    
    public bool IsDeleted { get; set; }
}