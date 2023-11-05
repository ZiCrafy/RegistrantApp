using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditLogs : BBApi
{
    public AuditLogs(RaContext ef, IConfiguration config) : base(ef, config)
    {
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, DateOnly dateStart, DateOnly dateEnd, int index,
        int recordsByPage, string? search = "")
    {
        return Ok();
    }
}