﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Auto;
using RegistrantApp.Shared.PresentationLayer.Auto;

namespace RegistrantApp.Server.BLL;

public class AutoAdapter : BaseAdapter
{
    public AutoAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ICollection<ViewAuto>> Get(long idAccount, bool showDeleted)
    {
        var data = _ef.Autos
            .Include(x => x.OwnerAuto)
            .Where(auto => auto.OwnerAuto.AccountID == idAccount && auto.IsDeleted == showDeleted)
            .ToList();

        return data.Adapt<List<ViewAuto>>();
    }

    public async Task<ViewAuto> Create(dtoAutoCreate dto)
    {
        var auto = new Auto();
        dto.Adapt(auto);
        auto.OwnerAuto = await _ef.Accounts.FirstOrDefaultAsync(account => account.AccountID == dto.OwnerAutoId)!;
        await _ef.AddAsync(auto);
        await _ef.SaveChangesAsync();
        return auto.Adapt<ViewAuto>();
    }

    public async Task<ViewAuto> Update(dtoAutoUpdate dto)
    {
        var found = await _ef.Autos.FirstOrDefaultAsync(auto => auto.AutoID == dto.AutoID);

        dto.Adapt(found);

        if (dto.OwnerAutoId is not 0)
            found!.OwnerAuto = await _ef.Accounts.FirstOrDefaultAsync(account => account.AccountID == dto.OwnerAutoId);

        _ef.Update(found);
        await _ef.SaveChangesAsync();

        return found.Adapt<ViewAuto>();
    }

    public async Task Delete(IEnumerable<long> idsAuto)
    {
        foreach (var item in idsAuto)
        {
            var found = await _ef.Autos.FirstOrDefaultAsync(z => z.AutoID == item);
            if (found == null)
                continue;
            found.IsDeleted = true;
            _ef.Update(found);
        }
        
        await _ef.SaveChangesAsync();
    }
}