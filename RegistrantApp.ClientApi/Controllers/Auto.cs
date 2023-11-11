using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Auto;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Auto : BControllerRest
{
    public Auto(string connectionString) : base(connectionString)
    {
        routeController = "Auto";
    }

    /// <summary>
    /// Получение списко авто-машины у владельца аккаунта
    /// </summary>
    /// <param name="token"></param>
    /// <param name="idAccount">ID аккаунта</param>
    /// <param name="showDeleted">Показывать удаленные машины</param>
    /// <returns></returns>
    public async Task<RestResponse> Get(string token, long idAccount, bool showDeleted)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("idAccount", idAccount);
        options.AddParameter("showDeleted", showDeleted);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Создание машины
    /// </summary>
    /// <param name="token"></param>
    /// <param name="dto">Объект передачи</param>
    /// <returns></returns>
    public async Task<RestResponse> Create(string token, dtoAutoCreate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Create", Method.Post);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Обновление машины
    /// </summary>
    /// <param name="token"></param>
    /// <param name="dto">Объект передачи</param>
    /// <returns></returns>
    public async Task<RestResponse> Update(string token, dtoAutoUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Удаление машины
    /// </summary>
    /// <param name="token"></param>
    /// <param name="idsAuto">Массив ID машин</param>
    /// <returns></returns>
    public async Task<RestResponse> Delete(string token, long[] idsAuto)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsAuto);
        return await client.ExecuteAsync(options);
    }
    
}