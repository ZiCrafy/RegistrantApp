using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class Order
{
    [Key] public long OrderID { get; set; }
    public Contragent? Contragent { get; set; }
    public Account? Account { get; set; }
    public Auto? Auto { get; set; }
    public DateTime DateTimePlannedArrive { get; set; }
    public DateTime? DateTimeRegistration { get; set; }
    public DateTime? DateTimeArrived { get; set; }
    public DateTime? DateTimeStartOrder { get; set; }
    public DateTime? DateTimeEndOrder { get; set; }
    public DateTime? DateTimeLeft { get; set; }
    public ICollection<File>? Files { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public DateTime DateTimeLastUsed { get; set; }
    public OrderDetail OrderDetail { get; set; }
    public bool IsDeleted { get; set; }
}