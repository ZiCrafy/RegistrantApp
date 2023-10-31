using System.ComponentModel.DataAnnotations;

namespace RegistrantApp.Shared.Database;

public class OrderDetail
{
    [Key] public long OrderDetailID { get; set; }
    public string? NumRealese { get; set; }
    public string? CountPodons { get; set; }
    public string? PacketDocuments { get; set; }
    public string? TochkaLoad { get; set; }
    public string? Nomenclature { get; set; }
    public string? Size { get; set; }
    public string? Destination { get; set; }
    public string? TypeLoad { get; set; }
    public string? Description { get; set; }
    public Account? StoreKeeperAccount { get; set; }
}