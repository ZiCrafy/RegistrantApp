using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Contragents;
using RegistrantApp.Shared.PresentationLayer.Contragents;

namespace RegistrantApp.Server.BLL;

public class ContragentsAdapter : BaseAdapter
{
    public ContragentsAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ViewContragentPagination> Get(int index, int recordsByPage,
        bool showDeleted, string search = "")
    {
        var totalRecords = _ef.Contragents.Count(
            contragent =>
                contragent.Title.ToUpper().Contains(search.ToUpper()) &&
                contragent.IsDeleted == showDeleted
        );

        var conragents = _ef.Contragents
            .Where(contragent =>
                contragent.Title.ToUpper().Contains(search) &&
                contragent.IsDeleted == showDeleted
            )
            .OrderBy(contragent => contragent.Title)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToList()
            .Adapt<List<ViewContragent>>();

        var pagination = new ViewContragentPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            PageIndex = index,
            Contragents = conragents
        };

        return pagination;
    }

    public async Task<ViewContragentPagination> Get(int index, int recordsByPage,
        bool showDeleted)
    {
        var totalRecords = _ef.Contragents.Count(
            contragent =>
                contragent.IsDeleted == showDeleted
        );

        var contragent = _ef.Contragents
            .Where(contragent =>
                contragent.IsDeleted == showDeleted
            )
            .OrderBy(contragent => contragent.Title)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToList()
            .Adapt<List<ViewContragent>>();

        var pagination = new ViewContragentPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            PageIndex = index,
            Contragents = contragent
        };

        return pagination;
    }

    public async Task<ViewContragent> Create(dtoContragentCreate dto)
    {
        var contragent = new Contragent();
        dto.Adapt(contragent);

        await _ef.AddAsync(contragent);
        await _ef.SaveChangesAsync();
        return contragent.Adapt<ViewContragent>();
    }

    public async Task<ViewContragent> Update(dtoContragentUpdate dto)
    {
        var foundContragent =
            await _ef.Contragents.FirstOrDefaultAsync(contragent => contragent.ContragentID == dto.ContragentID);

        if (foundContragent == null)
            return null;

        dto.Adapt(foundContragent);

        _ef.Update(foundContragent);
        await _ef.SaveChangesAsync();

        return foundContragent.Adapt<ViewContragent>();
    }

    public async Task Delete(long[] idsContragents)
    {
        foreach (var item in idsContragents)
        {
            var foundContragent =
                await _ef.Contragents.FirstOrDefaultAsync(contragent => contragent.ContragentID == item);

            if (foundContragent == null)
                continue;

            foundContragent.IsDeleted = true;
            _ef.Update(foundContragent);
            await _ef.SaveChangesAsync();
        }
    }
}