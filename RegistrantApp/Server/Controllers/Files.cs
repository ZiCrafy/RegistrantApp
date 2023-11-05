using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Files;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Files : BBApi
{
    private readonly FilesAdapter _adapter;

    public Files(RaContext ef, IConfiguration config) : base(ef, config)
    {
        _adapter = new FilesAdapter(ef);
    }

    [HttpGet("GetFromDocuments")]
    public async Task<IActionResult> GetFromDocuments([FromHeader] string token, long idDocument, bool showDeleted)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.GetFromDocuments(idDocument, showDeleted);

        return StatusCode(200, view);
    }

    [HttpGet("GetFromOrder")]
    public async Task<IActionResult> GetFromOrder([FromHeader] string token, long idOrder, bool showDeleted)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.GetFromOrder(idOrder, showDeleted);

        return StatusCode(200, view);
    }

    [HttpGet("Download")]
    public async Task<IActionResult> Download([FromHeader] string token, string idFile)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Download(idFile);

        return view;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Download([FromHeader] string token, IFormFile form)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Upload(form);

        return StatusCode(200, view);
    }


    [HttpPut("AttachFile")]
    public async Task<IActionResult> AttachFile([FromHeader] string token, dtoFileAttach dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.AttachFile(dto);

        return StatusCode(200, view);
    }
}