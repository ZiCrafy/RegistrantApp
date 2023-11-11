using RegistrantApp.ClientApi.Controllers;

namespace RegistrantApp.ClientApi;

public class RApi
{
    /// <summary>
    /// Класс для работы с аккантами
    /// </summary>
    public Accounts Accounts { get; private set; }
    /// <summary>
    /// Класс для работы с машинами от водителей(аккаунтов)
    /// </summary>
    public Autos Autos { get; private set; }
    /// <summary>
    /// Класс для работы с контрагентами
    /// </summary>
    public Contragents Contragents { get; private set; }
    /// <summary>
    /// Класс для работы с документами
    /// </summary>
    public Documents Documents { get; private set; }
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public Files Files { get; private set; }
    /// <summary>
    /// Класс для работы с деталями заказа
    /// </summary>
    public OrderDetails OrderDetails { get; private set; }
    /// <summary>
    /// Класс для работы с заказами
    /// </summary>
    public Orders Orders { get; private set; }
    /// <summary>
    /// Действия с учетной записью
    /// </summary>
    public Security Security { get; private set; }
    /// <summary>
    /// Журналы аудита
    /// </summary>
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