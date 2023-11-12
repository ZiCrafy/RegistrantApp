using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.PresentationLayer.Audit;

namespace RegistrantApp.Server.BLL;

public class AuditAdapter : BaseAdapter
{
    public AuditAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ViewAuditPagination?> GetAsync(DateOnly dateStart, DateOnly dateEnd, int index, int recordsByPage)
    {
        var totalRecords = await _ef.Audit.CountAsync(audit =>
            audit.DateTimeEvent >= new DateTime(dateStart.Year, dateStart.Month, dateStart.Day)
            &&
            audit.DateTimeEvent <=
            new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day).AddDays(1));

        var data = await _ef.Audit
            .Where(audit => audit.DateTimeEvent >= new DateTime(dateStart.Year, dateStart.Month, dateStart.Day)
                            &&
                            audit.DateTimeEvent <=
                            new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day).AddDays(1)
            )
            .OrderBy(audit => audit.DateTimeEvent)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToListAsync();

        var pagination = new ViewAuditPagination()
        {
            Audits = data.Adapt<List<ViewAudit>>(),
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            PageIndex = index
        };

        return pagination;
    }

    public async Task<ViewAuditPagination?> GetAsync(DateOnly dateStart, DateOnly dateEnd, int index, int recordsByPage,
        string search)
    {
        var totalRecords = await _ef.Audit.CountAsync(audit =>
            (audit.DateTimeEvent >= new DateTime(dateStart.Year, dateStart.Month, dateStart.Day)
             &&
             audit.DateTimeEvent <=
             new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day).AddDays(1)
            )
            &&
            (audit.Property!.ToUpper().Contains(search.ToUpper())
             ||
             audit.ValueAfter!.ToUpper().Contains(search.ToUpper())
             ||
             audit.ValueBefore!.ToUpper().Contains(search.ToUpper())
             ||
             audit.OwnerEvent!.ToUpper().Contains(search.ToUpper())
            ));

        var data = await _ef.Audit
            .Where(audit => (audit.DateTimeEvent >= new DateTime(dateStart.Year, dateStart.Month, dateStart.Day)
                             &&
                             audit.DateTimeEvent <=
                             new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day).AddDays(1)
                            )
                            &&
                            (audit.Property!.ToUpper().Contains(search.ToUpper())
                             ||
                             audit.ValueAfter!.ToUpper().Contains(search.ToUpper())
                             ||
                             audit.ValueBefore!.ToUpper().Contains(search.ToUpper())
                             ||
                             audit.OwnerEvent!.ToUpper().Contains(search.ToUpper())
                            )
            )
            .OrderBy(audit => audit.DateTimeEvent)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToListAsync();

        var pagination = new ViewAuditPagination()
        {
            Audits = data.Adapt<List<ViewAudit>>(),
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            PageIndex = index
        };

        return pagination;
    }
}