using RestSharp;

namespace RegistrantApp.ClientApi.Controllers.Base;

public class BControllerRest
{
    protected RestClient client;
    protected string? route = "api/";
    protected string? routeController;
    protected BControllerRest(string connectionString)
        => client = new RestClient(connectionString);
}