using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Orders;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Orders : BControllerRest
{
    public Orders(string connectionString) : base(connectionString)
    {
        routeController = "Orders";
    }
    
    /// <summary>
    /// Получение списка заказов
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="startDate">С какой датты</param>
    /// <param name="startEnd">по какое</param>
    /// <param name="index">текущая страница</param>
    /// <param name="recordsByPage">Количество записей на одной странице</param>
    /// <param name="showDeleted">Показывать удаленных</param>
    /// <param name="search">Строка поиска</param>
    /// <returns> Объект ViewOrderPagination </returns>
    public async Task<RestResponse> GetAsync(string token, DateOnly startDate, DateOnly startEnd, int index,
        int recordsByPage, bool showDeleted, string search)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("startDate", startDate);
        options.AddParameter("startEnd", startEnd);
        options.AddParameter("index", index);
        options.AddParameter("recordsByPage", recordsByPage);
        options.AddParameter("showDeleted", showDeleted);
        options.AddParameter("search", search);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Создание заказа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи данных</param>
    /// <returns>Объект ViewOrder </returns>
    public async Task<RestResponse> CreateAsync(string token, dtoOrderCreate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Create", Method.Post);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Обновление заказа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи данных</param>
    /// <returns>Объект ViewOrder</returns>
    public async Task<RestResponse> UpdateAsync(string token, dtoOrderUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Удаление заказов
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idsOrders">Массив ID заказов для удаление</param>
    /// <returns>ОК - если данные удалены</returns>
    public async Task<RestResponse> DeleteAsync(string token, long[] idsOrders)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsOrders);
        return await client.ExecuteAsync(options);
    }
    
}