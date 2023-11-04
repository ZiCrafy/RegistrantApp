namespace RegistrantApp.Shared.PresentationLayer.Pagination;

public class PaginationBase
{
    public long TotalRecords { get; set; }
    public long TotalPages { get; set; }
    public long PageIndex { get; set; }
}