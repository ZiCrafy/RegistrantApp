using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.Controllers.Base;
using RegistrantApp.Server.Database.Base;

namespace RegistrantApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Contragent : BBApi
{
    public Contragent(RaContext ef, IConfiguration config) : base(ef, config)
    {
    }
    
    
}