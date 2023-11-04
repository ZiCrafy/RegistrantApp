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

    public async Task<ICollection<ViewContragent>> Get(int index, int recordsByPage,
        bool showDeleted, string search = "")
    {
        return new List<ViewContragent>();
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
            
            if(foundContragent == null)
                continue;

            foundContragent.IsDeleted = true;
            _ef.Update(foundContragent);
            await _ef.SaveChangesAsync();
        }
        
    } 
    
}