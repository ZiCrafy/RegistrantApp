using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.Database;

namespace RegistrantApp.Server.Controllers.Base;

public class BBApi : ControllerBase
{
    protected readonly LiteContext _ef;
    protected readonly IConfiguration _config;

    public BBApi(LiteContext ef, IConfiguration config)
    {
        _ef = ef;
        _config = config;
    }
}