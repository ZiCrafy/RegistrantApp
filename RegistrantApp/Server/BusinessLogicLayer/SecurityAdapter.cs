using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BusinessLogicLayer.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Security;
using RegistrantApp.Shared.PresentationLayer.Security;

namespace RegistrantApp.Server.BusinessLogicLayer;

public class SecurityAdapter : BaseAdapter
{
    public SecurityAdapter(RaContext ef) : base(ef)
    {
    }
    
    public async Task<AccessToken?> CreateSession(dtoCredentials dto)
    {
        var foundAccount = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.PhoneNumber.ToString() == dto.Login
                                            && account.Password == dto.Password);

        if (foundAccount == null)
            return null;

        var token = new Token()
        {
            TokenID = $"{Guid.NewGuid()} {Guid.NewGuid()}"
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty),
            DateTimeSessionStarted = DateTime.Now,
            DateTimeSessionExpired = DateTime.Now.AddHours(10),
            OwnerToken = foundAccount,
            IPv4 = dto.IpAdres,
            FingerPrint = dto.FingerPrint
        };

        await _ef.AddAsync(token);
        await _ef.SaveChangesAsync();

        return token.Adapt<AccessToken>();
    }

    public async Task<AccessToken?> EndSession(dtoAccessTokenFinished dto)
    {
        var foundSession = await _ef.Tokens
            .FirstOrDefaultAsync(session =>
            session.TokenID == dto.Token && session.DateTimeSessionExpired >= DateTime.Now);

        if (foundSession == null)
            return null;

        foundSession.DateTimeSessionExpired = DateTime.Now;

        _ef.Update(foundSession);
        await _ef.SaveChangesAsync();
        return foundSession.Adapt<AccessToken>();
    }

    public async Task<object?> ChangePassword(dtoChangeCredentialPassword dto)
    {
        throw new NotImplementedException();
    }
}