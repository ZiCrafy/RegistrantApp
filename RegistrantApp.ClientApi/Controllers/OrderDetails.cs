using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Orders;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class OrderDetails : BControllerRest
{
    public OrderDetails(string connectionString) : base(connectionString)
    {
        routeController = "OrderDetails";
    }

    /// <summary>
    /// Получение деталей заказа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public async Task<RestResponse> GetAsync(string token, long idOrder)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("idOrder", idOrder);
        return await client.ExecuteAsync(options);
    }

    /// <summary>
    /// Обновление деталей заказа
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto">Объект передачи данных</param>
    /// <returns></returns>
    public async Task<RestResponse> UpdateAsync(string token, dtoOrderDetailsUpdate dto)
    {
        var options = new RestRequest($"{route}/{routeController}/Update", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
}