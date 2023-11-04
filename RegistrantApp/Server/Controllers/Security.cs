using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BusinessLogicLayer.Security;
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
        
        return view != null ? StatusCode(200, view) : StatusCode(401, "Auth Failed");
    }

    public async Task<IActionResult> EndSession([FromBody] dtoAccessTokenFinished dto)
    {
        var result = await _adapter.EndSession(dto);
        
        return result ? StatusCode(200) : StatusCode(404);
    }
    
    
}