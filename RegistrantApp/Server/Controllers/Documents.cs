using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Documents;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Documents : BBApi
{
    private readonly DocumentAdapter _adapter;

    public Documents(RaContext ef, IConfiguration config, DocumentAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, long idAccount, bool showDeleted)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.GetAsync(idAccount, showDeleted);

        return view is null ? StatusCode(404, _config["msg.NoContent"]) : StatusCode(200, view);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoDocumentCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.CreateAsync(dto);

        return view is null ? StatusCode(400, _config["msg.documents.CreateError"]) : StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoDocumentUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.UpdateAsync(dto);

        return view is null ? StatusCode(400, _config["msg.documents.CreateError"]) : StatusCode(200, view);
    }

    [HttpPut("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsDocuments)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        await _adapter.DeleteAsync(idsDocuments);

        return StatusCode(200);
    }
}