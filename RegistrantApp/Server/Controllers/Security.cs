using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Security;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Security : BBApi
{
    private readonly SecurityAdapter _adapter;

    public Security(RaContext ef, IConfiguration config) : base(ef, config)
    {
        _adapter = new SecurityAdapter(_ef);
    }

    [HttpPost("CreateSession")]
    public async Task<IActionResult> CreateSession([FromBody] dtoCredentials dto)
    {
        var view = await _adapter.CreateSession(dto);

        var account = await _ef.Tokens
            .Include(x => x.OwnerToken)
            .FirstOrDefaultAsync(x => x.TokenID == view!.Token);

        return view != null ? StatusCode(200, view) : StatusCode(401, "Auth Failed");
    }

    [HttpPut("EndSession")]
    public async Task<IActionResult> EndSession([FromBody] dtoAccessTokenFinished dto)
    {
        var view = await _adapter.EndSession(dto);
        
        var account = await _ef.Tokens
            .Include(x => x.OwnerToken)
            .FirstOrDefaultAsync(x => x.TokenID == view!.Token);
        
        return view != null ? StatusCode(200, view) : StatusCode(404, "Not Found!");
    }

    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] dtoChangeCredentialPassword dto)
    {
        var view = await _adapter.ChangePassword(dto);

        return view != null ? StatusCode(200, view) : StatusCode(404, "Not Found!");
    }
}