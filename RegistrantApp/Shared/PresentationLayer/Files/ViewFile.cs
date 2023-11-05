namespace RegistrantApp.Shared.PresentationLayer.Files;

public class ViewFile
{
    private string fileId;
    public string FileID
    {
        get => fileId.ToUpper();
        set => fileId= value.ToUpper();
    }
    public string FileName { get; set; }
    public DateTime DateTimeUpload { get; set; }
}