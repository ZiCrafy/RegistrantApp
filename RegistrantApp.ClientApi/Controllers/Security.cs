using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Security;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Security : BControllerRest
{
    public Security(string connectionString) : base(connectionString)
    {
        routeController = "Security";
    }
    
    /// <summary>
    /// Создание сессии и получение токена
    /// </summary>
    /// <param name="dto">Объект педерачи данных</param>
    /// <returns>Объект AccessToken с временем действия токена в случае успешной авторизации</returns>
    public async Task<RestResponse> CreateSessionAsync(dtoCredentials dto)
    {
        var options = new RestRequest($"{route}/{routeController}/CreateSession", Method.Post);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Завершение сессии
    /// </summary>
    /// <param name="dto">Объект педерачи данных</param>
    /// <returns>Объект AccessToken с временем действия токена </returns>
    public async Task<RestResponse> EndSessionAsync(dtoAccessTokenFinished dto)
    {
        var options = new RestRequest($"{route}/{routeController}/EndSession", Method.Put);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Смена пароля у аккаунта
    /// </summary>
    /// <param name="dto">Объект педерачи данных</param>
    /// <returns>200 - если пароль успешно изменен</returns>
    public async Task<RestResponse> ChangePasswordAsync(dtoChangeCredentialPassword dto)
    {
        var options = new RestRequest($"{route}/{routeController}/ChangePassword", Method.Put);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }
    
}