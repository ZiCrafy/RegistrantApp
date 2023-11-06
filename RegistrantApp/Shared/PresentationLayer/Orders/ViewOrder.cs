namespace RegistrantApp.Shared.PresentationLayer.Orders;

public class ViewOrder
{
    public long OrderID { get; set; }
    public string? AccountFirstName { get; set; }
    public string? AccountMiddleName { get; set; }
    public string? AccountLastName { get; set; }

    public string? AutoTitle { get; set; }
    public string? AutoNumber { get; set; }

    public DateTime DateTimePlannedArrive { get; set; }
    public DateTime? DateTimeRegistration { get; set; }
    public DateTime? DateTimeArrived { get; set; }
    public DateTime? DateTimeStartOrder { get; set; }
    public DateTime? DateTimeEndOrder { get; set; }
    public DateTime? DateTimeLeft { get; set; }

    public string? Status => GetStatus();

    public string GetStatus()
    {
        return "";
    }
}