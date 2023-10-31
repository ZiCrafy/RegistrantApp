using Microsoft.AspNetCore.Mvc;
using RegistrantApp.Server.Database;

namespace RegistrantApp.Server.Controllers.Base;

public class BaseBaseApi : ControllerBase
{
    protected readonly LiteContext _ef;
    
    public BaseBaseApi(LiteContext ef)
    {
        _ef = ef;
    }
}