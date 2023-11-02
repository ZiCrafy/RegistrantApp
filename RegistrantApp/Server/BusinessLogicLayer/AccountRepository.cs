using RegistrantApp.Server.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.PresentationLayer.Accounts;

namespace RegistrantApp.Server.BusinessLogicLayer;

public class AccountRepository
{
    private readonly LiteContext _ef;

    public AccountRepository(LiteContext ef) =>
        _ef = ef;

    public async Task<ViewAccount> Add(dtoAccountCreate dto)
    {
        // process
        return new ViewAccount();
    }
    
    
    
}