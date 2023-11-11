using RegistrantApp.ClientApi.Controllers;

namespace RegistrantApp.ClientApi;

public class RApi
{
    public Accounts Accounts { get; private set; }
    public Autos Autos { get; private set; }
    public Contragents Contragents { get; private set; }
    public Documents Documents { get; private set; }
    public Files Files { get; private set; }
    public OrderDetails OrderDetails { get; private set; }
    public Orders Orders { get; private set; }
    public Security Security { get; private set; }
    public Audit Audit { get; private set; }
    
    public RApi(string connectionString)
    {
        Accounts = new Accounts(connectionString);
        Autos = new Autos(connectionString);
        Contragents = new Contragents(connectionString);
        Documents = new Documents(connectionString);
        Files = new Files(connectionString);
        OrderDetails = new OrderDetails(connectionString);
        Orders = new Orders(connectionString);
        Security = new Security(connectionString);
        Audit = new Audit(connectionString);
    }
    
    
}