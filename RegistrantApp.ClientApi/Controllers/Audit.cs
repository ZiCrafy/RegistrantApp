using RegistrantApp.ClientApi.Controllers.Base;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Audit : BControllerRest
{
    public Audit(string connectionString) : base(connectionString)
    {
        routeController = "AuditLogs";
    }

    public async Task<object> Get(string token, DateOnly dateStart, DateOnly dateEnd, int index,
        int recordsByPage, string? search)
    {
        return new NotImplementedException();
    }
}