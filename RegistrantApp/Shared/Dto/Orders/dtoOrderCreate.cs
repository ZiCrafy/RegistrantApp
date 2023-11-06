namespace RegistrantApp.Shared.Dto.Orders;

public class dtoOrderCreate
{
    public long? IdContragent { get; set; }
    public long? IdAccount { get; set; }
    public long? IdAuto { get; set; }
    public DateTime DateTimePlannedArrive { get; set; }
}