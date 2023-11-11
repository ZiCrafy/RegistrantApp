using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Orders;

namespace RegistrantApp.Server.Controllers;

public class Orders : BBApi
{
    private readonly OrdersAdapter _adapter;

    public Orders(RaContext ef, IConfiguration config, OrdersAdapter adapter) : base(ef, config)
    {
        _adapter = adapter;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromHeader] string token, DateOnly startDate, DateOnly startEnd, int index,
        int recordsByPage, bool showDeleted, string search = "")
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = string.IsNullOrEmpty(search)
            ? await _adapter.GetAsync(startDate, startEnd, index, recordsByPage, showDeleted)
            : await _adapter.GetAsync(startDate, startEnd, index, recordsByPage, showDeleted, search);

        return view is null ? StatusCode(404, _config["msg.NoContent"]) : StatusCode(200, view);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, [FromBody] dtoOrderCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.CreateAsync(session, dto);

        return view is null ? StatusCode(400, _config["msg.orders.CreateError"]) : StatusCode(200, view);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, [FromBody] dtoOrderUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.UpdateAsync(session, dto);

        return view is null ? StatusCode(400, _config["msg.orders.UpdateError"]) : StatusCode(200, view);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsOrders)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        await _adapter.DeleteAsync(session, idsOrders);
        return StatusCode(200);
    }
}