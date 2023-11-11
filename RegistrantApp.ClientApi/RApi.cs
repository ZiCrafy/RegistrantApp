using RegistrantApp.ClientApi.Controllers;

namespace RegistrantApp.ClientApi;

public class RApi
{
    public Accounts Accounts { get; private set; }
    public Auto Autos { get; private set; }

    public RApi(string connectionString)
    {
        Accounts = new Accounts(connectionString);
        Autos = new Auto(connectionString);
    }
    
    
}