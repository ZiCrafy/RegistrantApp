using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Orders;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetails : BBApi
{
    private readonly OrderDetailsAdapter _adapter;

    public OrderDetails(RaContext ef, IConfiguration config, OrderDetailsAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, long idOrder)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.GetAsync(idOrder);

        return view is null ? StatusCode(404, _config["msg.MoContent"]) : StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, [FromBody] dtoOrderDetailsUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401, _config["msg.InvalidToken"]);

        var view = await _adapter.UpdateAsync(dto);

        return view is null ? StatusCode(404, _config["msg.MoContent"]) : StatusCode(200, view);
    }
}