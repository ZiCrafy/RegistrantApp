namespace RegistrantApp.Shared.Dto.Files;

public class dtoFileAttach
{
    private string idFile;
    public string IdFile
    {
        get => idFile.ToUpper();
        set => idFile= value.ToUpper();
    }
    public long? IdOrder { get; set; }
    public long? IdDocument { get; set; }
}