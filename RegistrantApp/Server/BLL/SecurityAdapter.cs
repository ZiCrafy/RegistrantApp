using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Security;
using RegistrantApp.Shared.PresentationLayer.Security;
using RegistrantApp.Shared.Validators;

namespace RegistrantApp.Server.BLL;

public class SecurityAdapter : BaseAdapter
{
    public SecurityAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<AccessToken?> CreateSessionAsync(dtoCredentials dto)
    {
        var foundAccount = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.PhoneNumber.ToString() == dto.Login
                                            && account.Password == dto.Password);

        if (foundAccount is null)
            return null;

        var token = new Token()
        {
            TokenID = $"{Guid.NewGuid()} {Guid.NewGuid()}"
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty),
            DateTimeSessionStarted = DateTime.Now,
            DateTimeSessionExpired = DateTime.Now.AddHours(10),
            OwnerToken = foundAccount,
            IPv4 = dto.IpAdress,
            FingerPrint = dto.FingerPrint
        };

        await _ef.AddAsync(token);
        await _ef.SaveChangesAsync();

        return token.Adapt<AccessToken>();
    }

    public async Task<AccessToken?> EndSessionAsync(dtoAccessTokenFinished dto)
    {
        var foundSession = await _ef.Tokens
            .FirstOrDefaultAsync(session =>
                session.TokenID == dto.Token && session.DateTimeSessionExpired >= DateTime.Now);

        if (foundSession is null)
            return null;

        foundSession.DateTimeSessionExpired = DateTime.Now;

        _ef.Update(foundSession);
        await _ef.SaveChangesAsync();
        return foundSession.Adapt<AccessToken>();
    }

    public async Task<string?> ChangePasswordAsync(long idAccount, dtoChangeCredentialPassword dto)
    {
        var foundAccount = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == idAccount);

        if (foundAccount is null)
            return null;

        if (foundAccount.Password != MyValidator.CreateMD5(dto.OldPassword))
            return null;

        foundAccount.Password = dto.NewPassword;

        _ef.Update(foundAccount);
        await _ef.SaveChangesAsync();

        return "Пароль изменен";
    }
}