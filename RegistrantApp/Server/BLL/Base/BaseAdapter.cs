using RegistrantApp.Server.Database.Base;

namespace RegistrantApp.Server.BLL.Base;

public class BaseAdapter
{
    protected readonly RaContext _ef;

    protected BaseAdapter(RaContext ef)
        => _ef = ef;
}