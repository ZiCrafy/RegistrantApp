using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Database;
using RegistrantApp.Shared.Database;

namespace RegistrantApp.Server.Controllers.Base;

public class BBApi : ControllerBase
{
    protected readonly LiteContext _ef;
    protected readonly IConfiguration _config;

    public BBApi(LiteContext ef, IConfiguration config)
    {
        _ef = ef;
        _config = config;
    }


    [NonAction]
    public bool ValidateToken(string tokenID, out Token token)
    {
        token = _ef.Tokens
            .Include(entity => entity.OwnerToken)
            .FirstOrDefault(x => (x.TokenID == tokenID) && x.DateTimeSessionExpired >= DateTime.Now)!;
        
        return token != null!;
    }
}