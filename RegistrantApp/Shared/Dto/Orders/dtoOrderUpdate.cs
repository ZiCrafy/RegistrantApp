namespace RegistrantApp.Shared.Dto.Orders;

public class dtoOrderUpdate
{
    public long OrderID { get; set; }
    public long? IdContragent { get; set; }
    public long? IdAccount { get; set; }
    public long? IdAuto { get; set; }
    public DateTime DateTimePlannedArrive { get; set; }
    public DateTime? DateTimeRegistration { get; set; }
    public DateTime? DateTimeArrived { get; set; }
    public DateTime? DateTimeStartOrder { get; set; }
    public DateTime? DateTimeEndOrder { get; set; }
    public DateTime? DateTimeLeft { get; set; }
}