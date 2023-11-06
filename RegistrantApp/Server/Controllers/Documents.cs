using Microsoft.AspNetCore.DataProtection.Internal;
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
            return StatusCode(401);

        var view = await _adapter.Get(idAccount, showDeleted);

        return StatusCode(200, view);
        
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoDocumentCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Create(dto);

        return StatusCode(200, view);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoDocumentUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Update(dto);

        return StatusCode(200, view);
    }
    
    [HttpPut("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsDocument)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        await _adapter.Delete(idsDocument);

        return StatusCode(200);
    }
}