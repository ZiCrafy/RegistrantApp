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

    public Contragents(RaContext ef, IConfiguration config, ContragentsAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, int index, int recordsByPage,
        bool showDeleted, string search = "")
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = string.IsNullOrEmpty(search)
            ? await _adapter.GetAsync(index, recordsByPage, showDeleted)
            : await _adapter.GetAsync(index, recordsByPage, showDeleted, search);

        return view is null ? StatusCode(404, _config["msg.NoContent"]) : StatusCode(200, view);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoContragentCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.CreateAsync(session, dto);

        return view is null ? StatusCode(400, _config["msg.contragent.CreateError"]) : StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoContragentUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.UpdateAsync(session, dto);

        return view is null ? StatusCode(400, _config["msg.contragent.UpdateError"]) : StatusCode(200, view);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsContragents)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        await _adapter.Delete(session, idsContragents);

        return StatusCode(200);
    }
}