using RegistrantApp.Shared.PresentationLayer.Pagination;

namespace RegistrantApp.Shared.PresentationLayer.Audit;

public class ViewAuditPagination : PaginationBase
{
    public ICollection<ViewAudit> Audits { get; set; } = new List<ViewAudit>();
}