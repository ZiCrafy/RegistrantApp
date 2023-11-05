using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Contragents;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Contragents : BBApi
{
    private readonly ContragentsAdapter _adapter;
    public Contragents(RaContext ef, IConfiguration config) : base(ef, config)
    {
        _adapter = new ContragentsAdapter(ef);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, int index, int recordsByPage,
        bool showDeleted, string search = "")
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        return string.IsNullOrEmpty(search)
            ? StatusCode(200, await _adapter.Get(index, recordsByPage, showDeleted))
            : StatusCode(200, await _adapter.Get(index, recordsByPage, showDeleted, search));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoContragentCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Create(dto);
        
        return StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoContragentUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Update(dto);

        return StatusCode(200, view);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsContragents)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        await _adapter.Delete(idsContragents);

        return StatusCode(200);
    }
}