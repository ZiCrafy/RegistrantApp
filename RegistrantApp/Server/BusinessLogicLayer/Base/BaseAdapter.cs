using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;

namespace RegistrantApp.Server.BusinessLogicLayer.Base;

public class BaseAdapter
{
    protected readonly RaContext _ef;

    public BaseAdapter(RaContext ef)
        => _ef = ef;

    protected async void RegisterEvent(Account account, string obj, string? action, string? property,
        string? valueafter, string? valuebefore)
    {
        var @event = new Event()
        {
            DateTimeEvent = DateTime.Now,
            OwnerEvent = account,
            Object = obj,
            Action = action,
            Property = property,
            ValueBefore = valuebefore,
            ValueAfter = valueafter
        };

        await _ef.AddAsync(@event);
        await _ef.SaveChangesAsync();
    }
}