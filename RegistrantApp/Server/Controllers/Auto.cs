using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Auto;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Auto : BBApi
{
    private readonly AutoAdapter _adapter;
    
    public Auto(RaContext ef, IConfiguration config, AutoAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, long idAccount, bool showDeleted)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Get(idAccount, showDeleted);
        
        return StatusCode(200, view);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoAutoCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        var view = await _adapter.Create(dto);
        
        return StatusCode(200, view);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoAutoUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        var view = await _adapter.Update(dto);
        
        return StatusCode(200, view);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsAuto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        await _adapter.Delete(idsAuto);
        
        return StatusCode(200);
    }
}