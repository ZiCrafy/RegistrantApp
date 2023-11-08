using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Orders;

namespace RegistrantApp.Server.Controllers;

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
            return StatusCode(401);

        var view = await _adapter.Get(idOrder);
        
        return StatusCode(200, view);
    }

    public async Task<IActionResult> Update([FromHeader] string token, dtoOrderDetailsUpdate dto)
    {
        if (!ValidateToken(token, out var session))
            return StatusCode(401);

        var view = await _adapter.Update(dto);
        
        return StatusCode(200);
    }
}