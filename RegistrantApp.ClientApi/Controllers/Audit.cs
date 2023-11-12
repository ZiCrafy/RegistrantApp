using RegistrantApp.ClientApi.Controllers.Base;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Audit : BControllerRest
{
    public Audit(string connectionString) : base(connectionString)
    {
        routeController = "Audit";
    }

    public async Task<object> Get(string token, DateOnly dateStart, DateOnly dateEnd, int index,
        int recordsByPage, string? search)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("dateStart", dateStart);
        options.AddParameter("dateEnd", dateEnd);
        options.AddParameter("index", index);
        options.AddParameter("recordsByPage", recordsByPage);
        options.AddParameter("search", search);
        return await client.ExecuteAsync(options);
    }
}