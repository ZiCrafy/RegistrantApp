using RegistrantApp.ClientApi.Controllers.Base;
using RegistrantApp.Shared.Dto.Files;
using RestSharp;

namespace RegistrantApp.ClientApi.Controllers;

public class Files : BControllerRest
{
    public Files(string connectionString) : base(connectionString)
    {
        routeController = "Files";
    }
    
    /// <summary>
    /// Получение ID файлов из документов
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idDocument">ID документа</param>
    /// <param name="showDeleted">Показать удаленные файлы</param>
    /// <returns>Объект ICollection ViewFile </returns>
    public async Task<RestResponse> GetFromDocumentsAsync(string token, long idDocument, bool showDeleted)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("idDocument", idDocument);
        options.AddParameter("showDeleted", showDeleted);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Получение ID файлов из заказов
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idOrder">ID заказа</param>
    /// <param name="showDeleted">Показать удаленные файлы</param>
    /// <returns>Объект ICollection ViewFile </returns>
    public async Task<RestResponse> GetFromOrderAsync(string token, long idOrder, bool showDeleted)
    {
        var options = new RestRequest($"{route}/{routeController}/Get", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("idOrder", idOrder);
        options.AddParameter("showDeleted", showDeleted);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Загрузка файла
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idFile">ID файла</param>
    /// <returns></returns>
    public async Task<RestResponse> DownloadAsync(string token, string idFile)
    {
        var options = new RestRequest($"{route}/{routeController}/Download", Method.Get);
        options.AddHeader("token", token);
        options.AddParameter("idFile", idFile);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Загрузка файла на сервер
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="file">Массив байтов</param>
    /// <param name="fileName">Назване файла</param>
    /// <returns>Объект ViewFile если файл загрузился</returns>
    public async Task<RestResponse> UploadAsync(string token, byte[] file, string fileName)
    {
        var options = new RestRequest($"{route}/{routeController}/Upload", Method.Post);
        options.AddHeader("token", token);
        options.AddHeader("Content-Type", "multipart/form-data");
        options.RequestFormat = DataFormat.Json;
        options.AddFile("content", file, fileName);
        return await client.ExecuteAsync(options);
    }
    
    /// <summary>
    /// Прикрепить файл к документу или заказу
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<RestResponse> AttachFileAsync(string token, dtoFileAttach dto)
    {
        var options = new RestRequest($"{route}/{routeController}/AttachFile", Method.Put);
        options.AddHeader("token", token);
        options.AddBody(dto);
        return await client.ExecuteAsync(options);
    }


    /// <summary>
    /// Удаления файла
    /// </summary>
    /// <param name="token">Действующий токен с необходимами правами</param>
    /// <param name="idsFiles">Массив ID файлов для удаления</param>
    /// <returns></returns>
    public async Task<RestResponse> DeleteAsync(string token, string[] idsFiles)
    {
        var options = new RestRequest($"{route}/{routeController}/Delete", Method.Delete);
        options.AddHeader("token", token);
        options.AddBody(idsFiles);
        return await client.ExecuteAsync(options);
    }
    
}