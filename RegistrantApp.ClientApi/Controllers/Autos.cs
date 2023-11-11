using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Auto;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Autos : BControllerRest
{
    public Autos(string connectionString) : base(connectionString)
    {
        routeController = "Auto";
    }

    /// <summary>
    /// Получение списко авто-машины у владельца аккаунта
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idAccount">ID аккаунта</param>
    /// <param name="showDeleted">Показывать удаленные машины</param>
    /// <returns>Объект ICollection ViewAuto с данными о машине</returns>
    public async Task<RestResponse> GetAsync(string token, long idAccount, bool showDeleted)
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
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи</param>
    /// <returns>Объект ViewAuto с данными о машине</returns>
    public async Task<RestResponse> CreateAsync(string token, dtoAutoCreate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Create", Method.Post);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Обновление машины
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи</param>
    /// <returns>Объект ViewAuto с данными о машине</returns>
    public async Task<RestResponse> UpdateAsync(string token, dtoAutoUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Удаление машины
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idsAuto">Массив ID машин</param>
    /// <returns>ОК - если данные удалены</returns>
    public async Task<RestResponse> DeleteAsync(string token, long[] idsAuto)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsAuto);
        return await client.ExecuteAsync(options);
    }
    
}