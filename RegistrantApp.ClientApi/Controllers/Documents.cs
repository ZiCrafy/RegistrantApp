using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Documents;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Documents : BControllerRest
{
    public Documents(string connectionString) : base(connectionString)
    {
        routeController = "Documents";
    }
    
    /// <summary>
    /// Получить документы пользователя
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idAccount">ID аккаунт</param>
    /// <param name="showDeleted">Показать удаленные документы</param>
    /// <returns>Объект ICollecation ViewDocument </returns>
    public async Task<RestResponse> GetAsync(string token, long idAccount, bool showDeleted)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("idAccount", idAccount);
        options.AddParameter("showDeleted", showDeleted);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Создание документа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи</param>
    /// <returns></returns>
    public async Task<RestResponse> CreateAsync(string token, dtoDocumentCreate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Create", Method.Post);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Обновление документа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи</param>
    /// <returns></returns>
    public async Task<RestResponse> UpdateAsync(string token, dtoDocumentUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Удаление документа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idsDocuments">Массив ID документов для удаления</param>
    /// <returns>ОК - если данные удалены</returns>
    public async Task<RestResponse> DeleteAsync(string token, long[] idsDocuments)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsDocuments);
        return await client.ExecuteAsync(options);
    }
    
}