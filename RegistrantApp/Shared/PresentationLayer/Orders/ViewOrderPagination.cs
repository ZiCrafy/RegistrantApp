using RegistrantApp.Shared.PresentationLayer.Pagination;

namespace RegistrantApp.Shared.PresentationLayer.Orders;

public class ViewOrderPagination : PaginationBase
{
    public ICollection<ViewOrder> Orders { get; set; } = new List<ViewOrder>();
}