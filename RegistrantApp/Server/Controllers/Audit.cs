using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Audit : BBApi
{
    private AuditAdapter _adapter;

    public Audit(RaContext ef, IConfiguration config, AuditAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, DateOnly dateStart, DateOnly dateEnd, int index,
        int recordsByPage, string? search = "")
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);
        
        var view = string.IsNullOrEmpty(search)
            ? await _adapter.GetAsync(dateStart, dateEnd, index,
                recordsByPage)
            : await _adapter.GetAsync(dateStart, dateEnd, index,
                recordsByPage, search);

        return StatusCode(200, view);
    }
}