using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BusinessLogicLayer;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database;
using RegistrantApp.Shared.Dto.Accounts;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Accounts : BBApi
{
    private readonly AccountAdapter _adapter;

    public Accounts(LiteContext ef, IConfiguration config) : base(ef, config)
    {
        _adapter = new AccountAdapter(ef);
    }

    [HttpGet("GetFromId")]
    public async Task<IActionResult> Get([FromHeader] string token, long accountId)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Get(accountId);

        return StatusCode(200, view);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, int index, int recordsByPage, bool showEmployee,
        bool showDeleted, string search = "")
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        return string.IsNullOrEmpty(search)
            ? StatusCode(200, await _adapter.Get(index, recordsByPage, showEmployee, showDeleted))
            : StatusCode(200, await _adapter.Get(index, recordsByPage, showEmployee, showDeleted, search));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, [FromBody] dtoAccountCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Add(dto);

        return StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, [FromBody] dtoAccountUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Update(dto);
        return StatusCode(200, view);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsAccount)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        await _adapter.Delete(idsAccount);
        return StatusCode(200);
    }
}