using Microsoft.AspNetCore.Mvc;
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

    public Security(RaContext ef, IConfiguration config, SecurityAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpPost("CreateSession")]
    public async Task<IActionResult> CreateSession([FromBody] dtoCredentials dto)
    {
        var view = await _adapter.CreateSession(dto);
        
        return view != null ? StatusCode(200, view) : StatusCode(401, "Auth Failed");
    }

    [HttpPut("EndSession")]
    public async Task<IActionResult> EndSession([FromBody] dtoAccessTokenFinished dto)
    {
        var view = await _adapter.EndSession(dto);
        
        return view != null ? StatusCode(200, view) : StatusCode(404, "Not Found!");
    }

    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromHeader] string token, [FromBody] dtoChangeCredentialPassword dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        var view = await _adapter.ChangePassword(session.OwnerToken.AccountID, dto);

        return view != null ? StatusCode(200, view) : StatusCode(404, "Not Found!");
    }
}