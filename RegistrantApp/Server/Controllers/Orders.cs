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
        
        return string.IsNullOrEmpty(search)
            ? StatusCode(200, await _adapter.Get(startDate, startEnd, index, recordsByPage, showDeleted))
            : StatusCode(200, await _adapter.Get(startDate, startEnd, index, recordsByPage, showDeleted, search));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string token, dtoOrderCreate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        var view = await _adapter.Create(dto);
        return StatusCode(200, view);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromHeader] string token, dtoOrderUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        return StatusCode(200);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string token, long[] idsOrders)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);
        
        return StatusCode(200);
    }
}