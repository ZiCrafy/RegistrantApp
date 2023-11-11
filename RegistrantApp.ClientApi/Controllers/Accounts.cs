using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Accounts;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Accounts : BControllerRest
{
    public Accounts(string connectionString) : base(connectionString)
    {
        routeController = "Accounts";
    }
    
    /// <summary>
    /// Получение детальной инфораации о конекретном аккаунте
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="accountId">ИД аккаунта</param>
    /// <returns>Объект ViewAccount с данными пользователя</returns>
    public async Task<RestResponse> GetAsync(string token, long accountId)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("accountId", accountId);
        return await client.ExecuteAsync(options);
    }

    /// <summary>
    /// Получение списка аккаунта в формате пагинации
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="index">Номер страницы</param>
    /// <param name="recordsByPage">Количиство записей на одной странице</param>
    /// <param name="showEmployee">Показывать сотрудников</param>
    /// <param name="showDeleted">Показывать удаленные записи</param>
    /// <param name="search">Поисковой запрос</param>
    /// <returns>Объект ViewAccountPagination с данными пользователей</returns>
    public async Task<RestResponse> GetAsync(string token, int index, int recordsByPage, bool showEmployee,
        bool showDeleted, string search)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("index", index);
        options.AddParameter("recordsByPage", recordsByPage);
        options.AddParameter("showEmployee", showEmployee);
        options.AddParameter("showDeleted", showDeleted);
        options.AddParameter("search", search);
        return await client.ExecuteAsync(options);
    }


    /// <summary>
    /// Создание нового аккаунта, возращает модель созданного аккаунта
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи аккаунта</param>
    /// <returns>Объект ViewAccount с данными пользователя</returns>
    public async Task<RestResponse> CreateAsync(string token, dtoAccountCreate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Create", Method.Post);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Обновление конкретного аккаунта
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи аккаунта</param>
    /// <returns>Объект ViewAccount с данными пользователя</returns>
    public async Task<RestResponse> UpdateAsync(string token, dtoAccountUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Удаление аккаунта
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idsAccount">Массив ID аккаунтов для удаления</param>
    /// <returns>ОК - если данные удалены</returns>
    public async Task<RestResponse> DeleteAsync(string token, long[] idsAccount)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsAccount);
        return await client.ExecuteAsync(options);
    }
    
}