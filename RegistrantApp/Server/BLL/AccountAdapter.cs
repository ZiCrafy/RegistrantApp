using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.PresentationLayer.Accounts;

namespace RegistrantApp.Server.BLL;

public class AccountAdapter : BaseAdapter
{
    public AccountAdapter(RaContext ef) : base(ef)
    {
    }
    
    public async Task<ViewAccount> Get(long idAccount)
    {
        var found = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == idAccount);

        return found.Adapt<ViewAccount>();
    }

    public async Task<ViewAccountPagination> Get(int index, int recordsByPage, bool showEmployee,
        bool showDeleted, string search = "")
    {
        var totalRecords = _ef.Accounts.Count(account =>
            account.IsDeleted == showDeleted && account.IsEmployee == showEmployee);

        var data = _ef.Accounts
            .Where(account => account.IsDeleted == showDeleted && account.IsEmployee == showEmployee)
            .Skip(recordsByPage * index)
            .Take(recordsByPage)
            .ToList()
            .Adapt<ICollection<ViewAccount>>();

        var pagination = new ViewAccountPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            Accounts = data
        };

        return pagination;
    }

    public async Task<ViewAccountPagination> Get(int index, int recordsByPage, bool showEmployee,
        bool showDeleted)
    {
        var totalRecords = _ef.Accounts.Count(account =>
            account.IsDeleted == showDeleted && account.IsEmployee == showEmployee);

        var data = _ef.Accounts
            .Where(account => account.IsDeleted == showDeleted && account.IsEmployee == showEmployee)
            .Skip(recordsByPage * index)
            .Take(recordsByPage)
            .ToList()
            .Adapt<ICollection<ViewAccount>>();

        var pagination = new ViewAccountPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            Accounts = data
        };

        return pagination;
    }

    public async Task<ViewAccount> Add(dtoAccountCreate dto)
    {
        var account = new Account();
        dto.Adapt(account);

        await _ef.AddAsync(account);
        await _ef.SaveChangesAsync();
        return account.Adapt<ViewAccount>();
    }

    public async Task<ViewAccount> Update(dtoAccountUpdate dto)
    {
        var found = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.AccountID);

        dto.Adapt(found);

        if (dto.Password is not null)
            found!.Password = dto.Password;

        _ef.Update(found!);
        await _ef.SaveChangesAsync();

        return found.Adapt<ViewAccount>();
    }

    public async Task Delete(long[] idsAccount)
    {
        var listFound = _ef.Accounts
            .Where(account => idsAccount.Contains(account.AccountID)
                              && account.IsDeleted == false);
        if (!listFound.Any())
            return;

        await listFound.ForEachAsync(account =>
        {
            account.IsDeleted = true;
            _ef.Update(account);
        });

        await _ef.SaveChangesAsync();
    }
    
}