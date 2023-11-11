using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Database;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;

namespace RegistrantApp.Server.Controllers.Base;

public class BBApi : ControllerBase
{
    protected readonly RaContext _ef;
    protected readonly IConfiguration _config;

    public BBApi(RaContext ef, IConfiguration config)
    {
        _ef = ef;
        _config = config;
    }


    [NonAction]
    protected bool ValidateToken(string tokenID, out Token token)
    {
        token = _ef.Tokens
            .Include(entity => entity.OwnerToken)
            .FirstOrDefault(x => (x.TokenID == tokenID) && x.DateTimeSessionExpired >= DateTime.Now)!;

        return token != null!;
    }
}