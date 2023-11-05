using RegistrantApp.Shared.PresentationLayer.Pagination;

namespace RegistrantApp.Shared.PresentationLayer.Contragents;

public class ViewContragentPagination : PaginationBase
{
    public ICollection<ViewContragent> Contragents { get; set; } = new List<ViewContragent>();
}