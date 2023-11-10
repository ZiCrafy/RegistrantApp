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
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.GetAsync(idAccount, showDeleted);

        if (view == null)
            return StatusCode(404, _config["msg.NoContent"]);
        
        return StatusCode(200, view);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoAutoCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);
        
        var view = await _adapter.CreateAsync(dto);

        if (view == null)
            return StatusCode(400, _config["msg.auto.CreateError"]);
        
        return StatusCode(200, view);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoAutoUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);
        
        var view = await _adapter.UpdateAsync(dto);
        
        if (view == null)
            return StatusCode(400, _config["msg.auto.UpdateError"]);
        
        return StatusCode(200, view);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsAuto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        await _adapter.DeleteAsync(idsAuto);
        
        return StatusCode(200);
    }
}