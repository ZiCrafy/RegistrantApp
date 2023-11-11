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

    public Files(RaContext ef, IConfiguration config, FilesAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("GetFromDocuments")]
    public async Task<IActionResult> GetFromDocuments([FromHeader] string token, long idDocument, bool showDeleted)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.GetFromDocumentsAsync(idDocument, showDeleted);

        return view is null ? StatusCode(404, _config["msg.NoContent"]) : StatusCode(200, view);
    }

    [HttpGet("GetFromOrder")]
    public async Task<IActionResult> GetFromOrder([FromHeader] string token, long idOrder, bool showDeleted)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.GetFromOrderAsync(idOrder, showDeleted);

        return view is null ? StatusCode(404, _config["msg.NoContent"]) : StatusCode(200, view);
    }

    [HttpGet("Download")]
    public async Task<IActionResult> Download([FromHeader] string token, string idFile)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.DownloadAsync(idFile);

        return view is null ? StatusCode(404, _config["msg.NoContent"]) : StatusCode(200, view);
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Download([FromHeader] string token, IFormFile form)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.UploadAsync(form);

        return view is null ? StatusCode(404, _config["msg.files.FileFailedUpload"]) : StatusCode(200, view);
    }


    [HttpPut("AttachFile")]
    public async Task<IActionResult> AttachFile([FromHeader] string token, dtoFileAttach dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.AttachFileAsync(dto);

        return view is null ? StatusCode(404, _config["msg.files.FileFailedAttach"]) : StatusCode(200, view);
    }
    
    [HttpPut("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, string idFile)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

       // var view = await _adapter.AttachFileAsync(dto);

        return Ok();
    }
}