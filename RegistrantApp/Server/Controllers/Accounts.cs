using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database;
using RegistrantApp.Shared.Dto.Accounts;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Accounts : BaseBaseApi
{
    public Accounts(LiteContext ef) : base(ef)
    {
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromHeader] string? token, [FromBody] dtoAccountCreate dto)
    {
        
        return StatusCode(200);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromHeader] string? token, long[] idsAccount)
    {
        var listFound = _ef.Accounts
            .Where(account => idsAccount.Contains(account.AccountID) 
                              && account.IsDeleted == false);

        if (listFound.Count() == 0)
            return NotFound();

        await listFound.ForEachAsync(account =>
        {
            account.IsDeleted = true;
            _ef.Update(account);
        } );
        
        await _ef.SaveChangesAsync();
        return StatusCode(200);
    } 
}