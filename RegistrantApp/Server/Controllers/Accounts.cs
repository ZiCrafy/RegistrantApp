using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Accounts;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Accounts : BBApi
{
    private readonly AccountAdapter _adapter;

    public Accounts(RaContext ef, IConfiguration config, AccountAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("GetFromId")]
    public async Task<IActionResult> Get([FromHeader] string token, long accountId)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        if (accountId <= 0)
            return StatusCode(400, _config["msg.accounts.InvalidOptions"]);

        var view = await _adapter.GetAsync(accountId);

        if (view == null)
            return StatusCode(404, _config["msg.NoContent"]);

        return StatusCode(200, view);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, int index, int recordsByPage, bool showEmployee,
        bool showDeleted, string search = "")
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        if (index <= 0)
            return StatusCode(400, _config["msg.accounts.InvalidOptions"]);

        return string.IsNullOrEmpty(search)
            ? StatusCode(200, await _adapter.GetAsync(index, recordsByPage, showEmployee, showDeleted))
            : StatusCode(200, await _adapter.GetAsync(index, recordsByPage, showEmployee, showDeleted, search));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, [FromBody] dtoAccountCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.AddAsync(dto);

        if (view == null)
            return StatusCode(503, _config["msg.account.CreateError"]);

        return StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, [FromBody] dtoAccountUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.UpdateAsync(dto);

        if (view == null)
            return StatusCode(503, _config["msg.account.UpdateError"]);

        return StatusCode(200, view);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsAccount)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        await _adapter.DeleteAsync(idsAccount);
        return StatusCode(200);
    }
}