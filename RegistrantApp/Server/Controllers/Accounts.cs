using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database;
using RegistrantApp.Shared.Dto.Accounts;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Accounts : BBApi
{
    public Accounts(LiteContext ef, IConfiguration config) : base(ef, config)
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

        if (!listFound.Any())
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