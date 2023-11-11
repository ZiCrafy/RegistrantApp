using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Contragents;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Contragents : BControllerRest
{
    public Contragents(string connectionString) : base(connectionString)
    {
        routeController = "Contragents";
    }

    public async Task<RestResponse> Get(string token, int index, int recordsByPage,
        bool showDeleted, string search)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("index", index);
        options.AddParameter("recordsByPage", recordsByPage);
        options.AddParameter("showDeleted", showDeleted);
        options.AddParameter("search", search);
        return await client.ExecuteAsync(options);
    }
    
    public async Task<RestResponse> Create(string token, dtoContragentCreate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Create", Method.Post);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    public async Task<RestResponse> Create(string token, dtoContragentUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    public async Task<RestResponse> Delete(string token, long[] idsAccount)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsAccount);
        return await client.ExecuteAsync(options);
    }
    
}