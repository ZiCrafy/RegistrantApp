using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Database;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.PresentationLayer.Accounts;

namespace RegistrantApp.Server.BusinessLogicLayer;

public class AccountAdapter
{
    private readonly LiteContext _ef;

    public AccountAdapter(LiteContext ef) =>
        _ef = ef;


    public async Task<ViewAccount> Get(long idAccount)
    {
        // process
        return new ViewAccount();
    }

    public async Task<ViewAccount> Get(int index, int recordsByPage, bool showEmployee,
        bool showDeleted, string search = "")
    {
        // process
        return new ViewAccount();
    }

    public async Task<ViewAccount> Add(dtoAccountCreate dto)
    {
        var account = new Account();
        account.Adapt(dto);

        await _ef.AddAsync(account);
        await _ef.SaveChangesAsync();
        return account.Adapt<ViewAccount>();
    }

    public async Task<ViewAccount> Update(dtoAccountUpdate dto)
    {
        var found = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.AccountID);
       
        found.Adapt(dto);
        
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