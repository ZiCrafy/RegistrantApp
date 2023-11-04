using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.Dto.Security;
using RegistrantApp.Shared.PresentationLayer.Security;

namespace RegistrantApp.Server.BusinessLogicLayer.Security;

public class SecurityAdapter
{
    private readonly RaContext _ef;

    public SecurityAdapter(RaContext ef)
        => _ef = ef;

    public async Task<AccessToken> CreateSession(dtoCredentials dto)
    {
        var foundAccount = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.PhoneNumber.ToString() == dto.Login
                                            && account.Password == dto.Password);

        if (foundAccount == null)
            return null;

        var token = new Token()
        {
            TokenID = $"{Guid.NewGuid()} {Guid.NewGuid()}".Replace("-", string.Empty),
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

    public async Task<bool> EndSession(dtoAccessTokenFinished dto)
    {
        var foundSession = await _ef.Tokens.FirstOrDefaultAsync(session =>
            session.TokenID == dto.Token && session.DateTimeSessionExpired >= DateTime.Now);

        if (foundSession == null)
            return false;

        foundSession.DateTimeSessionExpired = DateTime.Now;

        _ef.Update(foundSession);
        await _ef.SaveChangesAsync();
        return true;
    }
}